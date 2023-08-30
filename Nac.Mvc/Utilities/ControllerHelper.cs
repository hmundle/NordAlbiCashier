using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nac.Models.Entities;

namespace Nac.Mvc.Utilities;

public static class ControllerHelper
{
    private const int MAX_REQUEST_SEARCH_PANE_LENGTH = 20;

    public class DataTableSearchPaneContent
    {
        public string Label { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public static IEnumerable<DataTableSearchPaneContent> ProductCategoryValues { get; }
        = Enum.GetNames(typeof(ProductCategory)).Select(x => new DataTableSearchPaneContent { Label = x, Value = x });
    public static IEnumerable<DataTableSearchPaneContent> PaymentTypeValues { get; }
        = Enum.GetNames(typeof(PaymentType)).Select(x => new DataTableSearchPaneContent { Label = x, Value = x });
    public static IEnumerable<DataTableSearchPaneContent> SyncStatusValues { get; }
        = Enum.GetNames(typeof(SyncStatus)).Select(x => new DataTableSearchPaneContent { Label = x, Value = x });
    public static IEnumerable<DataTableSearchPaneContent> ProductGroupValues { get; }
        = Enum.GetNames(typeof(ProductGroup)).Select(x => new DataTableSearchPaneContent { Label = x, Value = x });

    public static SelectList ProductCategorySelectionList()
    => new(Enum.GetNames(typeof(ProductCategory)).Where(c => c != nameof(ProductCategory.Undefined)));
    public static SelectList PaymentTypeSelectionList()
    => new(Enum.GetNames(typeof(PaymentType)).Where(c => c != nameof(PaymentType.Undefined)));
    public static SelectList SyncStatusSelectionList()
    => new(Enum.GetNames(typeof(SyncStatus)));
    public static SelectList ProductGroupSelectionList()
    => new(Enum.GetNames(typeof(ProductGroup)).Where(c => c != nameof(ProductGroup.Undefined)));

    public class DataTablesRequest
    {
        public int TotalRecord { get; set; } = 0;
        public int FilterRecord { get; set; } = 0;
        public string? Draw { get; set; } = null;
        public string? SortColumn { get; set; } = string.Empty;
        public bool SortColumnDirectionIsAsc { get; set; } = true;
        public string? SearchValue { get; set; } = string.Empty;
        public int PageSize { get; set; } = 0;
        public int Skip { get; set; } = 0;
    };

    public class DataTablesSearchBuilderCondition
    {
        public string? OrigData { get; set; } = String.Empty;
        public string? Type { get; set; } = String.Empty;
        public string? Condition { get; set; } = String.Empty;
        public string? Value1 { get; set; } = string.Empty;
        public string? Value2 { get; set; } = string.Empty;
    };

    public static DataTablesRequest ExtractDataTablesRequest(HttpRequest request)
    {
        return new DataTablesRequest()
        {
            TotalRecord = 0,
            FilterRecord = 0,
            Draw = request.Form["draw"].FirstOrDefault(),
            SortColumn = request.Form["columns[" + request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault(),
            SortColumnDirectionIsAsc = request.Form["order[0][dir]"].FirstOrDefault()?.ToLower() != "desc",
            SearchValue = request.Form["search[value]"].FirstOrDefault(),
            PageSize = Convert.ToInt32(request.Form["length"].FirstOrDefault() ?? "0"),
            Skip = Convert.ToInt32(request.Form["start"].FirstOrDefault() ?? "0")
        };
    }

    public static IList<TEnum> ExtractFilterEnum<TEnum>(HttpRequest request, string field) where TEnum : struct
    {
        List<TEnum> enumList = new();
        for (int i = 0; i < MAX_REQUEST_SEARCH_PANE_LENGTH; i++)
        {
            string? strValue = request.Form["searchPanes[" + field + "][" + i + "]"].FirstOrDefault();
            if (strValue == null)
            {
                break;
            }
            if (Enum.TryParse(strValue, true, out TEnum enumValue))
            {
                enumList.Add(enumValue);
            }
        }

        return enumList;
    }

    public static IList<string> ExtractFilterString(HttpRequest request, string field)
    {
        List<string> stringList = new();
        for (int i = 0; i < MAX_REQUEST_SEARCH_PANE_LENGTH; i++)
        {
            string? strValue = request.Form["searchPanes[" + field + "][" + i + "]"].FirstOrDefault();
            if (strValue == null)
            {
                break;
            }
            stringList.Add(strValue);
        }

        return stringList;
    }

    public static IList<DataTablesSearchBuilderCondition> ExtractSearchBuilderConditions(HttpRequest request)
    {
        List<DataTablesSearchBuilderCondition> list = new();
        for (int i = 0; i < MAX_REQUEST_SEARCH_PANE_LENGTH; i++)
        {
            string strCriteriaBase = "searchBuilder[criteria][" + i + "]";
            string? strOrigData = request.Form[strCriteriaBase + "[origData]"].FirstOrDefault();
            if (strOrigData == null)
            {
                // no more criteria found, stop here
                break;
            }
            list.Add(new()
            {
                OrigData = strOrigData,
                Type = request.Form[strCriteriaBase + "[type]"].FirstOrDefault(),
                Condition = request.Form[strCriteriaBase + "[condition]"].FirstOrDefault(),
                Value1 = request.Form[strCriteriaBase + "[value1]"].FirstOrDefault(),
                Value2 = request.Form[strCriteriaBase + "[value2]"].FirstOrDefault(),
            });
        }
        return list;
    }

    public static (DateOnly, DateOnly) ConvertToDateValues(DataTablesSearchBuilderCondition condition)
    {
        DateOnly from = DateOnly.MinValue;
        DateOnly to = DateOnly.MinValue;

        // check for correct type and condition
        if (condition.Type != "date" || condition.Condition != "between")
        {
            return (from, to);
        }
        // convert values
        _ = DateOnly.TryParse(condition.Value1, out from);
        _ = DateOnly.TryParse(condition.Value2, out to);

        return (from, to);
    }

    public static string ConvertToLikeValue(DataTablesSearchBuilderCondition condition)
    {
        // check for correct type and condition
        if (condition.Type != "string" || condition.Condition != "contains")
        {
            return string.Empty;
        }
        if (condition.Value1 == null || condition.Value1!.Length < 3)
        {
            return string.Empty;
        }
        condition.Value1 = condition.Value1.Trim();
        if (condition.Value1!.Length < 3)
        {
            return string.Empty;
        }
        return "%" + condition.Value1 + "%";
    }

    public static int? ConvertToEqualNumValue(DataTablesSearchBuilderCondition condition)
    {
        if (condition.Type != "num")
            return null;

        if (condition.Value1 == null || condition.Value1!.Length < 1)
            return null;

        int number;
        switch (condition.Condition)
        {
            case "=":
            case ">":
            case ">=":
            case "<":
            case "<=":
                _ = int.TryParse(condition.Value1, out number);
                break;
            default:
                return null;
        };

        return number;
    }

    public static (int?, int?) ConvertToBetweenNumValues(DataTablesSearchBuilderCondition condition)
    {
        // check for correct type and condition
        if (condition.Type != "num" || condition.Condition != "between")
        {
            return (null, null);
        }
        if (condition.Value1 == null || condition.Value1!.Length < 1 || condition.Value2 == null || condition.Value2!.Length < 1)
        {
            return (null, null);
        }
        // convert values
        _ = int.TryParse(condition.Value1, out int from);
        _ = int.TryParse(condition.Value2, out int to);

        return (from, to);
    }

    public static IQueryable<T> QueryForNumProperty<T>(DataTablesSearchBuilderCondition condition, IQueryable<T> query, string propertyName)
    {
        if (condition.Condition != "between")
        {
            var numValue = ConvertToEqualNumValue(condition);
            if (numValue == null)
                return query;
            switch (condition.Condition)
            {
                default:
                    break;
                case "=":
                    query = query.Where(x => EF.Property<int>(x!, propertyName) == numValue);
                    break;
                case ">":
                    query = query.Where(x => EF.Property<int>(x!, propertyName) > numValue);
                    break;
                case ">=":
                    query = query.Where(x => EF.Property<int>(x!, propertyName) >= numValue);
                    break;
                case "<":
                    query = query.Where(x => EF.Property<int>(x!, propertyName) < numValue);
                    break;
                case "<=":
                    query = query.Where(x => EF.Property<int>(x!, propertyName) <= numValue);
                    break;
            }
        }
        else
        {
            var (fromNum, toNum) = ConvertToBetweenNumValues(condition);
            if (fromNum == null || toNum == null)
                return query;
            query = query.Where(x => EF.Property<int>(x!, propertyName) >= fromNum && EF.Property<int>(x!, propertyName) <= toNum);
        }

        return query;
    }

    public static IQueryable<T> QueryForDateOnlyProperty<T>(DataTablesSearchBuilderCondition condition, IQueryable<T> query, string propertyName)
    {
        var (from, to) = ConvertToDateValues(condition);
        if (from != DateOnly.MinValue && to != DateOnly.MinValue)
        {
            query = query.Where(x => EF.Property<DateOnly>(x!, propertyName) >= from && EF.Property<DateOnly>(x!, propertyName) <= to);
        }
        else if (from != DateOnly.MinValue)
        {
            query = query.Where(x => EF.Property<DateOnly>(x!, propertyName) >= from);
        }
        else if (to != DateOnly.MinValue)
        {
            query = query.Where(x => EF.Property<DateOnly>(x!, propertyName) <= to);
        }

        return query;
    }

    public static IQueryable<T> QueryForDateTimeProperty<T>(DataTablesSearchBuilderCondition condition, IQueryable<T> query, string propertyName)
    {
        //get values from SearchBuilder with DateOnly type
        var (fromDateOnly, toDateOnly) = ConvertToDateValues(condition);
        //then convert these values to DateTime type to use them in query for filtering.
        var from = fromDateOnly.ToDateTime(new TimeOnly(), DateTimeKind.Utc);
        var to = toDateOnly.ToDateTime(new TimeOnly(), DateTimeKind.Utc);
        if (to != DateTime.MinValue)
        {
            // for a valid end, we add 24h to get all products in that period as well
            to = to.AddDays(1);
        }
        if (from != DateTime.MinValue && to != DateTime.MinValue)
        {
            query = query.Where(x => EF.Property<DateTime>(x!, propertyName) >= from && EF.Property<DateTime>(x!, propertyName) < to);
        }
        else if (from != DateTime.MinValue)
        {
            query = query.Where(x => EF.Property<DateTime>(x!, propertyName) >= from);
        }
        else if (to != DateTime.MinValue)
        {
            query = query.Where(x => EF.Property<DateTime>(x!, propertyName) < to);
        }

        return query;
    }

    public static IQueryable<T> QueryForGuidProperty<T>(DataTablesSearchBuilderCondition condition, IQueryable<T> query, string propertyName)
    {
        var likeStr = ConvertToLikeValue(condition);
        if (likeStr.Length > 2) // the like string is "%str%", but it needs to be non-empty
        {
            query = query.Where(x => EF.Functions.ILike(EF.Property<Guid>(x!, propertyName).ToString()!, likeStr));
        }
        return query;
    }

    public static IQueryable<T> QueryForStringProperty<T>(DataTablesSearchBuilderCondition condition, IQueryable<T> query, string propertyName)
    {
        var likeStr = ConvertToLikeValue(condition);
        if (likeStr.Length > 2) // the like string is "%str%", but it needs to be non-empty
        {
            query = query.Where(x => EF.Functions.ILike(EF.Property<string>(x!, propertyName)!, likeStr));
        }
        return query;
    }
}

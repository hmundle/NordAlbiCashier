namespace Nac.Dal.Initialization;

public static class SampleData
{
    public static List<User> Users => new()
    {
        new() {Id = new("57B3B544-87B5-43C8-8FD4-DF312C201257"), Name = "Kasse1" },
        new() {Id = new("2E915F44-1E10-4AF3-9D0F-3E4E5AF7B857"), Name = "Kasse2" },
        new() {Id = new("BF46ED44-7909-43EC-AF7C-3581F08D5824"), Name = "Kasse3" },
    };

    public static List<Product> Products => new()
    {
        new() {Id = new("57B3B5F6-77B5-43C8-8FD4-DF312C201257"), Category = ProductCategory.Code, BarCode = "10001", Name = "Butter", Price = 2.5, PriceReduced = 2.0 },
        new() {Id = new("2E915FEA-0E10-4AF3-9D0F-3E4E5AF7B857"), Category = ProductCategory.Code, BarCode = "10002", Name = "Müsli", Price = 3.5, PriceReduced = null },
        new() {Id = new("BF46ED54-6909-43EC-AF7C-3581F08D5824"), Category = ProductCategory.Price, BarCode = "10003", Name = "Vollkornbrot", Price = 0, PriceReduced = null },
        new() {Id = new("F00DCA8C-1D22-417E-8275-D25897FD4F44"), Category = ProductCategory.Price, BarCode = "10004", Name = "Bratwurst", Price = 0, PriceReduced = null },
        new() {Id = new("5C7C8652-CD87-4AAB-B820-87BF7DAC9143"), Category = ProductCategory.Quantity, BarCode = "10005", Name = "Eier", Price = 1.0, PriceReduced = null },
        new() {Id = new("57B3B5F6-7FFF-43C8-8FD4-DF312C201257"), Category = ProductCategory.Quantity, BarCode = "10006", Name = "Apfel", Price = 0.75, PriceReduced = null },
        new() {Id = new("8F5DC134-8460-4B68-9E71-481935720333"), Category = ProductCategory.Weight, BarCode = "10007", Name = "Melone", Price = 4.5, PriceReduced = 4.0 },
        new() {Id = new("7A9265E9-9B83-43F6-BB9E-F4BABD468B94"), Category = ProductCategory.Weight, BarCode = "10008", Name = "Käse", Price = 22.50, PriceReduced = null },
        new() {Id = new("648D580D-B57D-43AF-8981-5E9F96E3CC2B"), Category = ProductCategory.Code, BarCode = "10009", Name = "Eistee", Price = 2.95, PriceReduced = null },
    };

    public static List<Invoice> Invoices => new()
    {
        new() {Id = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), Type = PaymentType.Cash },
        new() {Id = new("2E915FEA-2E10-4AF3-9D0F-3E4E5AF7B857"), Type = PaymentType.Cash },
        new() {Id = new("BF46ED54-8909-43EC-AF7C-3581F08D5824"), Type = PaymentType.PayPal },
    };

    public static List<Selling> Sellings => new()
    {
        new() {Id = new("57B3B5F6-87B5-43C8-8FD4-DF312C201257"), ProductId = new("57B3B5F6-77B5-43C8-8FD4-DF312C201257"), InvoiceId = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), Quantity = 2, PriceManual = 5.80, Weight = 0.150, FinalPrice = 2.95},
        new() {Id = new("2E915FEA-1E10-4AF3-9D0F-3E4E5AF7B857"), ProductId = new("2E915FEA-0E10-4AF3-9D0F-3E4E5AF7B857"), InvoiceId = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), Quantity = 2, PriceManual = 5.80, Weight = 0.150, FinalPrice = 2.95},
        new() {Id = new("BF46ED54-7909-43EC-AF7C-3581F08D5824"), ProductId = new("BF46ED54-6909-43EC-AF7C-3581F08D5824"), InvoiceId = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), Quantity = 2, PriceManual = 5.80, Weight = 0.150, FinalPrice = 2.95},
        new() {Id = new("F00DCA8C-2D22-417E-8275-D25897FD4F44"), ProductId = new("F00DCA8C-1D22-417E-8275-D25897FD4F44"), InvoiceId = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), Quantity = 2, PriceManual = 5.80, Weight = 0.150, FinalPrice = 2.95},
        new() {Id = new("5C7C8652-DD87-4AAB-B820-87BF7DAC9143"), ProductId = new("5C7C8652-CD87-4AAB-B820-87BF7DAC9143"), InvoiceId = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), Quantity = 2, PriceManual = 5.80, Weight = 0.150, FinalPrice = 2.95},
        new() {Id = new("57B3B5F6-8FFF-43C8-8FD4-DF312C201257"), ProductId = new("57B3B5F6-7FFF-43C8-8FD4-DF312C201257"), InvoiceId = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), Quantity = 2, PriceManual = 5.80, Weight = 0.150, FinalPrice = 2.95},
        new() {Id = new("8F5DC134-8460-4B68-9E71-481935720333"), ProductId = new("8F5DC134-8460-4B68-9E71-481935720333"), InvoiceId = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), Quantity = 2, PriceManual = 5.80, Weight = 0.150, FinalPrice = 2.95},
        new() {Id = new("7A9265E9-9B83-43F6-BB9E-F4BABD468B94"), ProductId = new("7A9265E9-9B83-43F6-BB9E-F4BABD468B94"), InvoiceId = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), Quantity = 2, PriceManual = 5.80, Weight = 0.150, FinalPrice = 2.95},
        new() {Id = new("648D580D-B57D-43AF-8981-5E9F96E3CC2B"), ProductId = new("648D580D-B57D-43AF-8981-5E9F96E3CC2B"), InvoiceId = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), Quantity = 2, PriceManual = 5.80, Weight = 0.150, FinalPrice = 2.95},
    };

}

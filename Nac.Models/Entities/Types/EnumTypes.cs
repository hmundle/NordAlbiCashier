namespace Nac.Models.Entities;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum ProductCategory
{
    Undefined = 0,
    Code,
    Quantity,
    Price,
    Weight
}

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum PaymentType
{
    Undefined = 0,
    Pending,
    Cash,
    Card,
    PayPal
}

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum SyncStatus
{
    Local = 0,
    Server
}

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum ProductGroup
{
    Undefined = 0,
    Getraenke,
    Milchprodukte,
    Wurst,
    Brot,
    Trockennahrung,
    KaffeeTee,
    Fruehstueck,
    Suessigkeiten,
    Obst,
    Gemuese,
    DosenGlaeserTetra,
    HygieneReinigung,
    Sonstiges,
    Specials,
    Pfand,
}

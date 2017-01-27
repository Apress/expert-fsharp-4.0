open System.IO
open System.Net

let http (url : string) = 
    let req = WebRequest.Create(url)
    use resp = req.GetResponse()
    use stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    reader.ReadToEnd()
//val http : url:string -> string

let worldBankCountriesXmlPage1 = http "http://api.worldbank.org/country"
//val worldBankCountriesXmlPage1 : string =
//  "<?xml version="1.0" encoding="utf-8"?>
//<wb:countries page="1"+[24134 chars]

worldBankCountriesXmlPage1
//val it : string =
//  "<?xml version="1.0" encoding="utf-8"?>
//<wb:countries page="1" pages="6" per_page="50" total="264" xmlns:wb="http://www.worldbank.org">
//  <wb:country id="ABW">
//    <wb:iso2Code>AW</wb:iso2Code>
//    <wb:name>Aruba</wb:name>
//    <wb:region id="LCN">Latin America &amp; Caribbean (all income levels)</wb:region>
//    <wb:adminregion id="" />
//    <wb:incomeLevel id="NOC">High income: nonOECD</wb:incomeLevel>
//    <wb:lendingType id="LNX">Not classified</wb:lendingType>
//    <wb:capitalCity>Oranjestad</wb:capitalCity>
//    <wb:longitude>-70.0167</wb:longitude>
//    <wb:latitude>12.5167</wb:latitude>
//  </wb:country>
//  <wb:country id="AFG">
//    <wb:iso2Code>AF</wb:iso2Code>
//    <wb:name>Afghanistan</wb:name>
//    <wb:region id="SAS">South Asia</wb:region>
//    <wb:adminregion id="SAS">South Asia</wb:adminregion>
//    <wb:incomeLevel id="LIC">Low income</wb:incomeLevel>
//    <wb:lendingType id="IDX">IDA</wb:lendingType>
//    <wb:capitalCity>Kabul</wb:capitalCity>
//    <wb:longitude>69.1761</wb:longitude>
//    <wb:latitude>34.5228</wb:latitude>
//  </wb:country>
//  ...
//  <wb:country id="COG">
//    <wb:iso2Code>CG</wb:iso2Code>
//    <wb:name>Congo, Rep.</wb:name>
//    <wb:region id="SSF">Sub-Saharan Africa (all income levels)</wb:region>
//    <wb:adminregion id="SSA">Sub-Saharan Africa (developing only)</wb:adminregion>
//    <wb:incomeLevel id="LMC">Lower middle income</wb:incomeLevel>
//    <wb:lendingType id="IDB">Blend</wb:lendingType>
//    <wb:capitalCity>Brazzaville</wb:capitalCity>
//    <wb:longitude>15.2662</wb:longitude>
//    <wb:latitude>-4.2767</wb:latitude>
//  </wb:country>
//</wb:countries>"

#I @"./packages/FSharp.Data/lib/net40"
#r "FSharp.Data.dll"
open FSharp.Data

type CountriesXml = XmlProvider<"http://api.worldbank.org/country">
let sampleCountries = CountriesXml.GetSample()
let sampleCountries' = CountriesXml.Load("http://api.worldbank.org/country")
//--> Added 'C:\...\13ExternalData\./packages/FSharp.Data/lib/net40' to library include path
//--> Referenced 'C:\...\13ExternalData\./packages/FSharp.Data/lib/net40\FSharp.Data.dll'
//type CountriesXml = FSharp.Data.XmlProvider<...>
//val sampleCountries : FSharp.Data.XmlProvider<...>.Countries =
//  <wb:countries page="1" pages="6" per_page="50" total="264" xmlns:wb="http://www.worldbank.org">
//  <wb:country id="ABW">
//  </wb:country>
//</wb:countries>
//val sampleCountries' : XmlProvider<...>.Countries =
//  <wb:countries page="1" pages="6" per_page="50" total="264" xmlns:wb="http://www.worldbank.org">
//  <wb:country id="ABW">
//  </wb:country>
//</wb:countries>

sampleCountries.Countries.Length
//val it : int = 50

sampleCountries.Countries.[0].Name
//val it : string = "Aruba"

let worldBankCountriesJsonPage1 = http "http://api.worldbank.org/country?format=json"
//val worldBankCountriesJsonPage1 : string =
//  "[{"page":1,"pages":6,"per_page":"50","total":264},[{"id":"ABW"+[16726 chars]

type CountriesJson = JsonProvider<"http://api.worldbank.org/country?format=json">
let sampleCountriesFromJson = CountriesJson.GetSample()
let sampleCountriesFromJson' = CountriesJson.Load("http://api.worldbank.org/country?format=json")
//type CountriesJson = JsonProvider<...>
//val sampleCountriesFromJson : JsonProvider<...>.Root =
//  [
//  {
//    "page": 1,
//    "pages": 6,
//    "per_page": "50",
//    "total": 264
//  },
//  [
//    {
//      "id": "ABW",
//      "iso2Code": "AW",
//      "name": "Aruba",
//...
//    }
//  ]
//]
sampleCountriesFromJson.Array.Length
//val it : int = 50

sampleCountriesFromJson.Array.[0].Name
//val it : string = "Aruba"

// NOTE: To avoid the following error, add a reference to System.Xml.Linq.
//The type referenced through 'System.Xml.Linq.XElement' is defined in an assembly that is not referenced. You must add a reference to assembly 'System.Xml.Linq'.
#r "System.Xml.Linq.dll"

let worldBankCountriesXmlPage2 = http "http://api.worldbank.org/country?page=2"
let loadPageFromXml n = CountriesXml.Load(sprintf "http://api.worldbank.org/country?page=%d" n)

let countries = 
    let page1 = loadPageFromXml 1
    [ for i in 1 .. page1.Pages do 
         let page = loadPageFromXml i
         yield! page.Countries ]

//--> Referenced 'C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Xml.Linq.dll'
//val worldBankCountriesXmlPage2 : string =
//  "<?xml version="1.0" encoding="utf-8"?>
//<wb:countries page="2"+[24311 chars]
//val loadPageFromXml : n:int -> XmlProvider<...>.Countries
//val countries : XmlProvider<...>.Country list =
//  [<wb:country id="ABW" xmlns:wb="http://www.worldbank.org">
//  <wb:iso2Code>AW</wb:iso2Code>
//  <wb:name>Aruba</wb:name>
//  ...
//  <wb:longitude />
//  <wb:latitude />
//</wb:country>;
//   ...]

countries.Length
//val it : int = 256

[ for c in countries -> c.Name ]
//val it : string list =
//  ["Aruba"; "Afghanistan"; "Africa"; "Angola"; "Albania"; "Andorra";
//   "Andean Region"; "Arab World"; "United Arab Emirates"; "Argentina";
//   "Armenia"; "American Samoa"; "Antigua and Barbuda"; "Australia"; "Austria";
//   "Azerbaijan"; "Burundi"; "Belgium"; "Benin"; "Burkina Faso"; "Bangladesh";
//   "Bulgaria"; "Bahrain"; "Bahamas, The"; "Bosnia and Herzegovina"; "Belarus";
//   "Belize"; "Bermuda"; "Bolivia"; "Brazil"; "Barbados"; "Brunei Darussalam";
//   "Bhutan"; "Botswana"; "Sub-Saharan Africa (IFC classification)";
//   "Central African Republic"; "Canada";
//   "East Asia and the Pacific (IFC classification)";
//   "Central Europe and the Baltics";
//   "Europe and Central Asia (IFC classification)"; "Switzerland";
//   "Channel Islands"; "Chile"; "China"; "Cote d'Ivoire";
//   "Latin America and the Caribbean (IFC classification)";
//   "Middle East and North Africa (IFC classification)"; "Cameroon";
//   "Congo, Dem. Rep."; "Congo, Rep."; "Colombia"; "Comoros"; "Cabo Verde";
//   "Costa Rica"; "South Asia (IFC classification)"; "Caribbean small states";
//   "Cuba"; "Curacao"; "Cayman Islands"; "Cyprus"; "Czech Republic"; "Germany";
//   "Djibouti"; "Dominica"; "Denmark"; "Dominican Republic"; "Algeria";
//   "East Asia & Pacific (developing only)";
//   "East Asia & Pacific (all income levels)";
//   "Europe & Central Asia (developing only)";
//   "Europe & Central Asia (all income levels)"; "Ecuador"; "Egypt, Arab Rep.";
//   "Euro area"; "Eritrea"; "Spain"; "Estonia"; "Ethiopia"; "European Union";
//   "Fragile and conflict affected situations"; "Finland"; "Fiji"; "France";
//   "Faeroe Islands"; "Micronesia, Fed. Sts."; "Gabon"; "United Kingdom";
//   "Georgia"; "Ghana"; "Guinea"; "Gambia, The"; "Guinea-Bissau";
//   "Equatorial Guinea"; "Greece"; "Grenada"; "Greenland"; "Guatemala"; "Guam";
//   "Guyana"; "High income"; ...]

let loadPageFromJson n = CountriesJson.Load(sprintf "http://api.worldbank.org/country?format=json&page=%d" n)

let countriesFromJson = 
    let page1 = loadPageFromJson 1
    [ for i in 1 .. page1.Record.Pages do 
         let page = loadPageFromJson i
         yield! (page.Array |> Seq.map (fun x -> x.Name)) ]
//val countriesFromJson : string list =
//  ["Aruba"; "Afghanistan"; "Africa"; "Angola"; "Albania"; "Andorra";
//   "Andean Region"; "Arab World"; "United Arab Emirates"; "Argentina";
//   "Armenia"; "American Samoa"; "Antigua and Barbuda"; "Australia"; "Austria";
//   "Azerbaijan"; "Burundi"; "Belgium"; "Benin"; "Burkina Faso"; "Bangladesh";
//   "Bulgaria"; "Bahrain"; "Bahamas, The"; "Bosnia and Herzegovina"; "Belarus";
//   "Belize"; "Bermuda"; "Bolivia"; "Brazil"; "Barbados"; "Brunei Darussalam";
//   "Bhutan"; "Botswana"; "Sub-Saharan Africa (IFC classification)";
//   "Central African Republic"; "Canada";
//   "East Asia and the Pacific (IFC classification)";
//   "Central Europe and the Baltics";
//   "Europe and Central Asia (IFC classification)"; "Switzerland";
//   "Channel Islands"; "Chile"; "China"; "Cote d'Ivoire";
//   "Latin America and the Caribbean (IFC classification)";
//   "Middle East and North Africa (IFC classification)"; "Cameroon";
//   "Congo, Dem. Rep."; "Congo, Rep."; "Colombia"; "Comoros"; "Cabo Verde";
//   "Costa Rica"; "South Asia (IFC classification)"; "Caribbean small states";
//   "Cuba"; "Curacao"; "Cayman Islands"; "Cyprus"; "Czech Republic"; "Germany";
//   "Djibouti"; "Dominica"; "Denmark"; "Dominican Republic"; "Algeria";
//   "East Asia & Pacific (developing only)";
//   "East Asia & Pacific (all income levels)";
//   "Europe & Central Asia (developing only)";
//   "Europe & Central Asia (all income levels)"; "Ecuador"; "Egypt, Arab Rep.";
//   "Euro area"; "Eritrea"; "Spain"; "Estonia"; "Ethiopia"; "European Union";
//   "Fragile and conflict affected situations"; "Finland"; "Fiji"; "France";
//   "Faeroe Islands"; "Micronesia, Fed. Sts."; "Gabon"; "United Kingdom";
//   "Georgia"; "Ghana"; "Guinea"; "Gambia, The"; "Guinea-Bissau";
//   "Equatorial Guinea"; "Greece"; "Grenada"; "Greenland"; "Guatemala"; "Guam";
//   "Guyana"; "High income"; ...]

let loadPageFromJson1000 = 
    CountriesJson.Load("http://api.worldbank.org/country?format=json&per_page=1000")
//val loadPageFromJson1000 : JsonProvider<...>.Root =
//  [
//  {
//    "page": 1,
//    "pages": 1,
//    "per_page": "1000",
//    "total": 264
//  },
//  [
//    {
//      "id": "ABW",
//      "iso2Code": "AW",
//      "name": "Aruba",
//      ...
//    },
//    ...
//    {
//      "id": "ZWE",
//      "iso2Code": "ZW",
//      "name": "Zimbabwe",
//      ...
//    }
//  ]
//]

// NOTE: The northwind database can be downloaded from Microsoft. The download page is described as
// "Northwind and pubs Sample Databases for SQL Server 2000". The MSI installs both *.mdf/*.ldf and
// scripts for this database and the pubs database by default, to the file system at
// C:\SQL Server 2000 Sample Databases. If installing into a later version of SQL server then
// the install script will error but a workaround is available,
// SEE: http://www.howtosolutions.net/2013/07/solving-install-northwind-database-on-sql-server-problem.
// NOTE: The fix is to replace the following two lines with the third line ...
//exec sp_dboption 'Northwind','trunc. log on chkpt.','true'
//exec sp_dboption 'Northwind','select into/bulkcopy','true'
//alter database Northwind set recovery simple

#r "System.Data.Linq.dll"
#r "FSharp.Data.TypeProviders.dll"

open Microsoft.FSharp.Linq
open Microsoft.FSharp.Data.TypeProviders

type NorthwndDb = SqlDataConnection<ConnectionString = @"Server=.;Database=Northwind;Trusted_Connection=True;", Pluralize=true>
let db = NorthwndDb.GetDataContext()
db.DataContext.Log <- System.Console.Out

let customersSortedByCountry = 
    query { for c in db.Customers do 
            sortBy c.Country
            select (c.Country, c.CompanyName) }
    |> Seq.toList

let selectedEmployees = 
    query { for emp in db.Employees do
            where (emp.BirthDate.Value.Year > 1960)
            where (emp.LastName.StartsWith "S")
            select (emp.FirstName, emp.LastName) 
            take 5 }
    |> Seq.toList

let customersSortedTwoColumns = 
    query { for c in db.Customers do 
            sortBy c.Country
            thenBy c.Region
            select (c.Country, c.Region, c.CompanyName) }
    |> Seq.toList

let totalOrderQuantity =
    query { for order in db.OrderDetails do
            sumBy (int order.Quantity) }

let customersAverageOrders = 
    query { for c in db.Customers do 
            averageBy (float c.Orders.Count) }

let averagePriceOverProductRange =
    query { for p in db.Products do
            averageByNullable p.UnitPrice }

open System
let totalOrderQuantity' =
    query { for c in db.Customers do 
            let numOrders = 
                query { for o in c.Orders do 
                        for od in o.OrderDetails do 
                        sumByNullable (Nullable(int od.Quantity)) }
            let averagePrice = 
                query { for o in c.Orders do 
                        for od in o.OrderDetails do 
                        averageByNullable (Nullable(od.UnitPrice)) }
            select (c.ContactName, numOrders, averagePrice) }
    |> Seq.toList

let productsGroupedByNameAndCountedTest1 =
    query { for p in db.Products do
            groupBy p.Category.CategoryName into group
            let sum = 
                query { for p in group do
                        sumBy (int p.UnitsInStock.Value) }
            select (group.Key, sum) }
    |> Seq.toList

let innerJoinQuery = 
    query { for c in db.Categories do
            join p in db.Products on (c.CategoryID =? p.CategoryID) 
            select (p.ProductName, c.CategoryName) } 
    |> Seq.toList

let innerJoinQuery' = 
    query { for p in db.Products do
            select (p.ProductName, p.Category.CategoryName) }
    |> Seq.toList

let innerGroupJoinQueryWithAggregation =
    query { for c in db.Categories do
            groupJoin p in db.Products on (c.CategoryID =? p.CategoryID) into prodGroup
            let groupMax = query { for p in prodGroup do maxByNullable p.UnitsOnOrder }
            select (c.CategoryName, groupMax) }
    |> Seq.toList

//--> Referenced 'C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Data.Linq.dll'
//--> Referenced 'C:\Program Files (x86)\Reference Assemblies\Microsoft\FSharp\.NETFramework\v4.0\4.3.0.0\Type Providers\FSharp.Data.TypeProviders.dll'
//
//SELECT [t0].[Country] AS [Item1], [t0].[CompanyName] AS [Item2]
//FROM [dbo].[Customers] AS [t0]
//ORDER BY [t0].[Country]
//-- Context: SqlProvider(Sql2008) Model: AttributedMetaModel Build: 4.6.81.0
//
//SELECT TOP (5) [t0].[FirstName] AS [Item1], [t0].[LastName] AS [Item2]
//FROM [dbo].[Employees] AS [t0]
//WHERE ([t0].[LastName] LIKE @p0) AND (DATEPART(Year, [t0].[BirthDate]) > @p1)
//-- @p0: Input NVarChar (Size = 4000; Prec = 0; Scale = 0) [S%]
//-- @p1: Input Int (Size = -1; Prec = 0; Scale = 0) [1960]
//-- Context: SqlProvider(Sql2008) Model: AttributedMetaModel Build: 4.6.81.0
//
//SELECT [t0].[Country] AS [Item1], [t0].[Region] AS [Item2], [t0].[CompanyName] AS [Item3]
//FROM [dbo].[Customers] AS [t0]
//ORDER BY [t0].[Country], [t0].[Region]
//-- Context: SqlProvider(Sql2008) Model: AttributedMetaModel Build: 4.6.81.0
//
//SELECT SUM(CONVERT(Int,[t0].[Quantity])) AS [value]
//FROM [dbo].[Order Details] AS [t0]
//-- Context: SqlProvider(Sql2008) Model: AttributedMetaModel Build: 4.6.81.0
//
//SELECT AVG([t2].[value]) AS [value]
//FROM (
//    SELECT CONVERT(Float,(
//        SELECT COUNT(*)
//        FROM [dbo].[Orders] AS [t1]
//        WHERE [t1].[CustomerID] = [t0].[CustomerID]
//        )) AS [value]
//    FROM [dbo].[Customers] AS [t0]
//    ) AS [t2]
//-- Context: SqlProvider(Sql2008) Model: AttributedMetaModel Build: 4.6.81.0
//
//SELECT AVG([t0].[UnitPrice]) AS [value]
//FROM [dbo].[Products] AS [t0]
//-- Context: SqlProvider(Sql2008) Model: AttributedMetaModel Build: 4.6.81.0
//
//SELECT [t0].[ContactName] AS [Item1], (
//    SELECT SUM([t3].[value])
//    FROM (
//        SELECT CONVERT(Int,[t2].[Quantity]) AS [value], [t1].[CustomerID], [t2].[OrderID], [t1].[OrderID] AS [OrderID2]
//        FROM [dbo].[Orders] AS [t1], [dbo].[Order Details] AS [t2]
//        ) AS [t3]
//    WHERE ([t3].[CustomerID] = [t0].[CustomerID]) AND ([t3].[OrderID] = [t3].[OrderID2])
//    ) AS [Item2], (
//    SELECT AVG([t6].[value])
//    FROM (
//        SELECT [t5].[UnitPrice] AS [value], [t4].[CustomerID], [t5].[OrderID], [t4].[OrderID] AS [OrderID2]
//        FROM [dbo].[Orders] AS [t4], [dbo].[Order Details] AS [t5]
//        ) AS [t6]
//    WHERE ([t6].[CustomerID] = [t0].[CustomerID]) AND ([t6].[OrderID] = [t6].[OrderID2])
//    ) AS [Item3]
//FROM [dbo].[Customers] AS [t0]
//-- Context: SqlProvider(Sql2008) Model: AttributedMetaModel Build: 4.6.81.0
//
//SELECT SUM(CONVERT(Int,[t0].[UnitsInStock])) AS [Item2], [t1].[CategoryName] AS [Item1]
//FROM [dbo].[Products] AS [t0]
//LEFT OUTER JOIN [dbo].[Categories] AS [t1] ON [t1].[CategoryID] = [t0].[CategoryID]
//GROUP BY [t1].[CategoryName]
//-- Context: SqlProvider(Sql2008) Model: AttributedMetaModel Build: 4.6.81.0
//
//SELECT [t1].[ProductName] AS [Item1], [t0].[CategoryName] AS [Item2]
//FROM [dbo].[Categories] AS [t0]
//INNER JOIN [dbo].[Products] AS [t1] ON ([t0].[CategoryID]) = [t1].[CategoryID]
//-- Context: SqlProvider(Sql2008) Model: AttributedMetaModel Build: 4.6.81.0
//
//SELECT [t0].[ProductName] AS [Item1], [t1].[CategoryName] AS [Item2]
//FROM [dbo].[Products] AS [t0]
//LEFT OUTER JOIN [dbo].[Categories] AS [t1] ON [t1].[CategoryID] = [t0].[CategoryID]
//-- Context: SqlProvider(Sql2008) Model: AttributedMetaModel Build: 4.6.81.0
//
//SELECT [t0].[CategoryName] AS [Item1], (
//    SELECT MAX([t1].[UnitsOnOrder])
//    FROM [dbo].[Products] AS [t1]
//    WHERE ([t0].[CategoryID]) = [t1].[CategoryID]
//    ) AS [Item2]
//FROM [dbo].[Categories] AS [t0]
//-- Context: SqlProvider(Sql2008) Model: AttributedMetaModel Build: 4.6.81.0
//
//
//type NorthwndDb =
//  class
//    static member GetDataContext : unit -> NorthwndDb.ServiceTypes.SimpleDataContextTypes.Northwind
//     + 1 overload
//    nested type ServiceTypes
//  end
//val db : NorthwndDb.ServiceTypes.SimpleDataContextTypes.Northwind
//val customersSortedByCountry : (string * string) list =
//  [("Argentina", "Cactus Comidas para llevar");
//   ("Argentina", "Océano Atlántico Ltda."); ("Argentina", "Rancho grande");
//...
//   ("Venezuela", "LINO-Delicateses"); ("Venezuela", "HILARION-Abastos");
//   ("Venezuela", "GROSELLA-Restaurante")]
//val selectedEmployees : (string * string) list = [("Michael", "Suyama")]
//val customersSortedTwoColumns : (string * string * string) list =
//  [("Argentina", null, "Cactus Comidas para llevar");
//   ("Argentina", null, "Océano Atlántico Ltda.");
//...
//   ("Venezuela", "Nueva Esparta", "LINO-Delicateses");
//   ("Venezuela", "Táchira", "HILARION-Abastos")]
//val totalOrderQuantity : int = 51317
//val customersAverageOrders : float = 9.120879121
//val averagePriceOverProductRange : System.Nullable<decimal> = 28.8663M
//val totalOrderQuantity' :
//  (string * System.Nullable<int> * System.Nullable<decimal>) list =
//  [("Maria Anders", 174, 26.7375M); ("Ana Trujillo", 63, 21.5050M);
//   ("Antonio Moreno", 359, 21.7194M); ("Thomas Hardy", 650, 19.1766M);
//...
//   ("Karl Jablonski", 1063, 31.9582M); ("Matti Karttunen", 148, 25.1588M);
//   ("Zbyszek Piestrzeniewicz", 205, 20.6312M)]
//val productsGroupedByNameAndCountedTest1 : (string * int) list =
//  [("Beverages", 559); ("Condiments", 507); ("Confections", 386);
//   ("Dairy Products", 393); ("Grains/Cereals", 308); ("Meat/Poultry", 165);
//   ("Produce", 100); ("Seafood", 701)]
//val innerJoinQuery : (string * string) list =
//  [("Chai", "Beverages"); ("Chang", "Beverages");
//   ("Aniseed Syrup", "Condiments");
//...
//   ("Lakkalikööri", "Beverages");
//   ("Original Frankfurter grüne Soße", "Condiments")]
//val innerJoinQuery' : (string * string) list =
//  [("Chai", "Beverages"); ("Chang", "Beverages");
//   ("Aniseed Syrup", "Condiments");
//...
//   ("Lakkalikööri", "Beverages");
//   ("Original Frankfurter grüne Soße", "Condiments")]
//val innerGroupJoinQueryWithAggregation :
//  (string * System.Nullable<int16>) list =
//  [("Beverages", 40s); ("Condiments", 100s); ("Confections", 70s);
//   ("Dairy Products", 70s); ("Grains/Cereals", 80s); ("Meat/Poultry", 0s);
//   ("Produce", 20s); ("Seafood", 70s)]


// NOTE: The adventure works sample database is available at http://msftdbprodsamples.codeplex.com.
// NOTE: This sample code has a connection string to the 2012 version of the sample database.

#I "./packages/FSharp.Data.SqlClient/lib/net40"
#r "FSharp.Data.SqlClient.dll"
open FSharp.Data

[<Literal>]
let connectionString = 
    @"Data Source=.;Initial Catalog=AdventureWorks2012;Integrated Security=True"

let cmd = 
    new SqlCommandProvider<
       "SELECT TOP(@topN) FirstName, LastName, SalesYTD 
        FROM Sales.vSalesPerson
        WHERE CountryRegionName = @regionName AND SalesYTD > @salesMoreThan 
        ORDER BY SalesYTD" , connectionString>()

cmd.Execute(topN = 3L, regionName = "United States", salesMoreThan = 1000000M) |> printfn "%A"
//--> Added 'E:\...\13ExternalData\./packages/FSharp.Data.SqlClient/lib/net40' to library include path
//--> Referenced 'E:\...\13ExternalData\./packages/FSharp.Data.SqlClient/lib/net40\FSharp.Data.SqlClient.dll'
//
//seq
//  [{ FirstName = "Pamela"; LastName = "Ansman-Wolfe"; SalesYTD = 1352577.1325M };
//   { FirstName = "David"; LastName = "Campbell"; SalesYTD = 1573012.9383M };
//   { FirstName = "Tete"; LastName = "Mensa-Annan"; SalesYTD = 1576562.1966M }]
//
//val connectionString : string =
//  "Data Source=.;Initial Catalog=AdventureWorks2012;Integrated S"+[12 chars]
//val cmd : FSharp.Data.SqlCommandProvider<...>
//val it : unit = ()

// NOTE: The book describes this section connecting to ".\SQLEXPRESS" as the server not "." as I do here.

open System.Data
open System.Data.SqlClient

// NOTE: Database is not set in this connection string because I cannot connect to a database that doesn't yet exist.
// System.Data.SqlClient.SqlException (0x80131904): Cannot open database "company" requested by the login. The login failed.
//let connString = @"Server='.';Database=company;Integrated Security=SSPI"
let connString = @"Server='.';Integrated Security=SSPI"
let conn = new SqlConnection(connString)

conn.Open()

open System.Data
open System.Data.SqlClient

let execNonQuery conn s =
    let comm = new SqlCommand(s, conn, CommandTimeout = 10)
    comm.ExecuteNonQuery() |> ignore
//val connString : string = "Server='.';Integrated Security=SSPI"
//val conn : SqlConnection = System.Data.SqlClient.SqlConnection
//val execNonQuery : conn:SqlConnection -> s:string -> unit

execNonQuery conn "CREATE DATABASE company"
//val it : unit = ()

// NOTE: At this stage I could stick with the same connection string or narrow it down to the database.
// If the former, then I need to add the TSQL USE statement.
//let connString = @"Server='.';Database=company;Integrated Security=SSPI"
execNonQuery conn "USE company"

execNonQuery conn """
CREATE TABLE Employees (
   EmpID int NOT NULL,
   FirstName varchar(50) NOT NULL,
   LastName varchar(50) NOT NULL,
   Birthday datetime,
   PRIMARY KEY (EmpID))"""
//val it : unit = ()

execNonQuery conn """
INSERT INTO Employees (EmpId, FirstName, LastName, Birthday)
   VALUES (1001, 'Joe', 'Smith', '02/14/1965')"""
//val it : unit = ()

execNonQuery conn """
INSERT INTO Employees (EmpId, FirstName, LastName, Birthday)
   VALUES (1002, 'Mary', 'Jones', '09/15/1985')"""
//val it : unit = ()

let query() =
    seq {
        use conn = new SqlConnection(connString)
        conn.Open()
        use comm = new SqlCommand("""
SELECT FirstName, Birthday FROM Employees""", conn)
        use reader = comm.ExecuteReader()
        while reader.Read() do
            yield (reader.GetString 0, reader.GetDateTime 1)
    }
//val query : unit -> seq<string * System.DateTime>

fsi.AddPrinter(fun (d : System.DateTime) -> d.ToString())
query()
//val it : seq<string * System.DateTime> =
//  seq [("Joe", 14/02/1965 12:00:00 AM); ("Mary", 15/09/1985 12:00:00 AM)]

query() |> Seq.iter (fun (fn, bday) -> printfn "%s has birthday %O" fn bday)
//Joe has birthday 14/02/1965 00:00:00
//Mary has birthday 15/09/1985 00:00:00
//val it : unit = ()

query()
  |> Seq.filter (fun (_, bday) -> bday < System.DateTime.Parse("01/01/1985"))
  |> Seq.length
//val it : int = 1

// NOTE: Cannot execute create procedure, preceded by a USE statement.
//System.Data.SqlClient.SqlException (0x80131904): 'CREATE/ALTER PROCEDURE' must be the first statement in a query batch.
execNonQuery conn """
CREATE PROCEDURE dbo.GetEmployeesByLastName ( @Name nvarchar(50) ) AS
    SELECT Employees.FirstName, Employees.LastName
    FROM Employees
    WHERE Employees.LastName LIKE @Name"""
//val it : unit = ()

let GetEmployeesByLastName (name : string) =
    use comm = new SqlCommand("GetEmployeesByLastName", conn,
                              CommandType = CommandType.StoredProcedure)

    comm.Parameters.AddWithValue("@Name", name) |> ignore
    use adapter = new SqlDataAdapter(comm)
    let table = new DataTable()
    adapter.Fill(table) |> ignore
    table
//val GetEmployeesByLastName : name:string -> DataTable

for row in GetEmployeesByLastName("Smith").Rows do
     printfn "row = %O, %O" (row.Item "FirstName") (row.Item "LastName")
//row = Joe, Smith
//val it : unit = ()
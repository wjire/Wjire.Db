1.only support : "sqlserver","mysql"

2.the configuration priority:
    appsettings.Development.json > appsettings.json

{
  "connectionStrings": {
    "masterRead": {
      "connectionString": "Data Source=localhost;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=Aa1111",
      "providerName": "sqlserver"
    },
    "masterWrite": {
      "connectionString": "Data Source=localhost;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=Aa1111",
      "providerName": "sqlserver"
    }
  }
}
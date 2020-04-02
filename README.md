# Blazor.C3jsChart
C3js component for Blazor

![Blazor.C3jsChart](/assets/demo-blazor.c3jsChart.png)

# Usage
## Dataset via markup sintaxe
```html
 <Chart TItem="double"
        Title="Pesquisas online"
        Type="ChartType.Pie">
   <ChartDataset Name="Acesso Mercado Livre" Datasource="datasourceAcessoML" />
   <ChartDataset Name="Acesso Bondfaro" Datasource="datasourceAcessoBf" />
   <ChartDataset Name="Acesso Buscape" Datasource="datasourceAcessoBP" />
   <ChartDataset Name="Acesso Zoom" Datasource="datasourceAcessoZoom" />
 </Chart>
```
```cs
@code {
  IList<double> datasourceAcessoML = new List<double> { 10500 };
  IList<double> datasourceAcessoBf = new List<double> { 3000 };
  IList<double> datasourceAcessoBP = new List<double> { 5000 };
  IList<double> datasourceAcessoZoom = new List<double> { 18000 };
}
```

The graphics generated by this code 
![Blazor.C3jsChart](/assets/ChartPieExample.png)

# Dataset via code
```html
 <Chart @ref="chart" TItem="double"
        Title="Pesquisas online"
        Type="ChartType.Pie"/>
```
```cs
@code {
  IList<double> datasourceAcessoML = new List<double> { 10500 };
  IList<double> datasourceAcessoBf = new List<double> { 3000 };
  IList<double> datasourceAcessoBP = new List<double> { 5000 };
  IList<double> datasourceAcessoZoom = new List<double> { 18000 };
  
  //reference to the object Chart
  Chart chart;
  
  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
       chart.AddDataset(new DataSet<double>
       {
           Name = "Acesso Mercado Livre",
           Datasouce = datasourceAcessoML  
        );
        
        //other way to do the same
        chart.AddDataset(chart.CreateDataset("Acesso Bondfaro", 3000));
        //or
        var buscarPeDataset = chart.CreateDataset("Acesso Buscape", 5000)
        chart.AddDataset(buscaPeDataset);
  }
}
```
Like you can see Chart library is very versatil, and you can work in the way that is better for you. 
# Instalation
For now to use clone the intire repository and run the default startup project in the solution.   
I not create a NuGet yet.    
Later I will produce them.    

To use in your solution copy the project Blazor.C3jsChart from the directory(folder) that you clone
the repository to your solution.

# Setting your project

In the _imports.razor add: 
```cs
@using Blazor.C3jsChart
```
In the _host.cshtml Blazor Server or index.cshtml for WebAssembly 
in the head section of the html
```html
<link href="_content/Blazor.C3jsChart/c3js/c3.min.css" rel="stylesheet" />
```
And before the tag body and after the blazor.js framework put: 
```html
    <script src="_content/Blazor.C3jsChart/C3jsChart.js"></script>
</body>
```
I'm wait for your contribution.
Thanks.

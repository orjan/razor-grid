Razor Grid
==========

The goal for Razor Grid to create a low friction integration point between [ASP.NET MVC](http://www.asp.net/mvc) and [jqGrid](http://www.trirand.com/blog/)

How does it work
----------------

**Controller**
The controller is responsible for the data that are return to jqGrid.  

*JqGridOptions*
Specifies the behaviour for pagination e.g. all grids should by default list 10 items

*JqGridData*

    var gridData = new JqGridData<Log>();
    gridData.IdFunction = l => l.Id;
	gridData.AddColumn("Id", l => l.Id, true);
	gridData.AddColumn("Message", l => l.Message, true);
	gridData.AddColumn("Logged", l => l.DateTime, false);
         

* Whether a column is sortable or not, due to performance issues for a large unindex result set
* The order of the columns since jqGrid requires that the data is returned in a specific order
* The value the will be evaluated for each grid view cell


*JqGridFilter*

Is created from the request from jqGrid, asking the controller to return the wanted data. This provides
the entry point for the integration with your DL.

* Page
* Number of items per page
* Order by column
* Ascending or Descending order

**View**
Since making changes to the view is very expensive and to keep the view as dry as possible a helper method has been created
under normal conditions the only code that needs to be added to the view is:
    
	@JqGrid.Render(
        caption: "Simple logs",
        options: @Model
    )

To make the configuration of the jqGrid as flexible as possible the view helper *JqGrid.cshtml* should be implemented for each project.

Sample 
------

A simple example application *RazorGrid.Example* has been created that creates a very simple grid with support for pagination and sorting
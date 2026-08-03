module exoplanetProject.ArgHandler

type searchOptions = 
    {
        Select: string list
        Range: int*int
        WhereClause: string option
        OrderBy: string option
    }
let defaultOptions = 
    {
        Select = ["pl_name"]
        Range = (0, 5)
        WhereClause = None
        OrderBy = None
    }
let parseArgs (args: string array) =
        defaultOptions

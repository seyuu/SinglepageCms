
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

public class BaseController : Controller {

    protected Db db {
        get;
        private set;
    }

    protected override void OnActionExecuting(ActionExecutingContext filterContext) {
        db = new Db();
        base.OnActionExecuting(filterContext);
    }

    protected override void OnResultExecuted(ResultExecutedContext filterContext) {
        base.OnResultExecuted(filterContext);
        db.Dispose();
    }

}

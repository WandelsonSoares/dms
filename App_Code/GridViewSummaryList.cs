//------------------------------------------------------------------------------------------
// Copyright © 2006 Agrinei Sousa [www.agrinei.com]
//
// Esse código fonte é fornecido sem garantia de qualquer tipo.
// Sinta-se livre para utilizá-lo, modificá-lo e distribuí-lo,
// inclusive em aplicações comerciais.
// É altamente desejável que essa mensagem não seja removida.
//------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace App_Code
{
    /// <summary>
    /// Summary description for GridViewSummaryList
    /// </summary>
    public class GridViewSummaryList : List<GridViewSummary>
    {
        public GridViewSummary this[string name]
        {
            get { return FindSummaryByColumn(name); }
        }

        public GridViewSummary FindSummaryByColumn(string columnName)
        {
            return this.FirstOrDefault(s => s.Column.ToLower() == columnName.ToLower());
        }
    }
}

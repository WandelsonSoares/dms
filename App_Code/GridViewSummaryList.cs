//------------------------------------------------------------------------------------------
// Copyright � 2006 Agrinei Sousa [www.agrinei.com]
//
// Esse c�digo fonte � fornecido sem garantia de qualquer tipo.
// Sinta-se livre para utiliz�-lo, modific�-lo e distribu�-lo,
// inclusive em aplica��es comerciais.
// � altamente desej�vel que essa mensagem n�o seja removida.
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

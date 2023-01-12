//------------------------------------------------------------------------------------------
// Copyright � 2006 Agrinei Sousa [www.agrinei.com]
//
// Esse c�digo fonte � fornecido sem garantia de qualquer tipo.
// Sinta-se livre para utiliz�-lo, modific�-lo e distribu�-lo,
// inclusive em aplica��es comerciais.
// � altamente desej�vel que essa mensagem n�o seja removida.
//------------------------------------------------------------------------------------------

using System.Linq;
using System.Collections.Generic;

namespace App_Code
{
    /// <summary>
    /// Summary description for GridViewGroupList
    /// </summary>
    public class GridViewGroupList : List<GridViewGroup>
    {
        public GridViewGroup this[string name]
        {
            get { return FindGroupByName(name); }
        }

        public GridViewGroup FindGroupByName(string name)
        {
            return this.FirstOrDefault(g => g.Name.ToLower() == name.ToLower());
        }
    }
}

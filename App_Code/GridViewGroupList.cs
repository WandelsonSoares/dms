//------------------------------------------------------------------------------------------
// Copyright © 2006 Agrinei Sousa [www.agrinei.com]
//
// Esse código fonte é fornecido sem garantia de qualquer tipo.
// Sinta-se livre para utilizá-lo, modificá-lo e distribuí-lo,
// inclusive em aplicações comerciais.
// É altamente desejável que essa mensagem não seja removida.
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

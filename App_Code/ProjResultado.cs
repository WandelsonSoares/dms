using System;

namespace App_Code
{
    /// <summary>
    /// Onde será executada as Functions do Forecast
    /// </summary>
    public class ProjResultado
    {
        readonly _linQ_ProjecaoDeResultado ProjResult = new _linQ_ProjecaoDeResultado();
        public string Resultado;

        public string ProjResultFat(int ContaID, int Mes, int Ano, int Perc, int ContratoID)
        {
            if (ContratoID > 0)
            {
                Resultado = Convert.ToString(ProjResult.FxForecast(ContaID, Mes, Ano, ContratoID, Perc));
            }
            if (ContratoID == 0)
            {
                Resultado = Convert.ToString(ProjResult.FxForecastBrasil_FAT(ContaID, Mes, Ano, Perc));
            }
            if (ContratoID < 0) //Para Agrupamentos Customizados
            {
                //Criar Functions
            }        

            return Resultado;
        }




    }
}

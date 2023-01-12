using System;
using App_Code;


public class cnSelect
{

    readonly cSession _sgcSess = new cSession();

    //public string NovoContraID()
    //{
    //    string BSelect = (from C in _linQ.Contratos select C.ContratoID).Max().ToString();
        
    //    int ContratoID = Convert.ToInt32(BSelect) + 1;
    //    return ContratoID.ToString();
    //}

    //public Array selectCentroNegocio(string cnID)
    //{
    //    var cNegocio = from D in _linQ.Contratos where D.ContratoID.ToString() == cnID select D;

    //    return cNegocio.ToArray();
    //}
    //public Array selectSituacaoContratual(string cnID)
    //{
    //    var cNegocio = from D in _linQ.Contratos where D.ContratoID.ToString() == cnID select D;

    //    return cNegocio.ToArray();
    //}
    //public Array selectDadosComerciais(string cnID)
    //{
    //    var cNegocio = from D in _linQ.Contratos_DadosComerciais where D.ContratoID.ToString() == cnID select D;

    //    return cNegocio.ToArray();
    //}

    //public Array selectGpNucleo(string cnID)
    //{
    //    var cNegocio = from D in _linQ.Contratos_Grupos where D.ContratoID.ToString() == cnID select D;

    //    return cNegocio.ToArray();
    //}
    //public Array selectGpCliente(string cnID)
    //{
    //    var cNegocio = from D in _linQ.Contratos_GruposClientes where D.ContratoID.ToString() == cnID select D;

    //    return cNegocio.ToArray();
    //}
    //public Array selectGpRegional(string cnID)
    //{
    //    var cNegocio = from D in _linQ.Contratos_Regionals where D.ContratoID.ToString() == cnID select D;

    //    return cNegocio.ToArray();
    //}
    //public Array selectParametroTipoCN(string cnID)
    //{
    //    var cNegocio = from D in _linQ.Contratos_Config_Tipos where D.ContratoID.ToString() == cnID select D;        
        
    //    return cNegocio.ToArray();
    //}
    //public Array selectParametroTipoFat(string cnID)
    //{
    //    var cNegocio = from D in _linQ.Contratos_Config_TipoFats where D.ContratoID.ToString() == cnID select D;       
        
    //    return cNegocio.ToArray();
    //}
    //public Array selectParametroClassCustoDesp(string cnID)
    //{
    //    var cNegocio = from D in _linQ.Contratos_class_desps where D.ContratoID.ToString() == cnID select D;

    //    return cNegocio.ToArray();
    //}

    //public bool selectMesRatFix(string cnIdIn, string cnIdOut, string Mes, string Ano)
    //{
    //    var Confirm = false;
    //    try
    //    {
    //        Confirm = Convert.ToBoolean((from D in _linQ.tbl_cad_FixRats where D.ContratoID_in.ToString() == cnIdIn && D.ContratoID_out.ToString() == cnIdOut && D.Mes.ToString() == Mes && D.Ano.ToString() == Ano select D.Mes).Single());
    //    }
    //    catch{}


    //    return Confirm;
    //}

    public string SumFaTouMbc(string contratoID, string fatouMbc)
    {
        var rskLinQ = new linQ_rsk_stps();

        var valor = rskLinQ.stp_rsk_sum_Forecast(Convert.ToInt32(contratoID), fatouMbc, Convert.ToInt32(_sgcSess.AnoAtual)).ToString();

        return valor;
    }

}
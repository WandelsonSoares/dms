#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3082
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;



[System.Data.Linq.Mapping.DatabaseAttribute(Name="SGC_NET")]
public partial class _linQ_ProjecaoDeResultado : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  #endregion
	
	public _linQ_ProjecaoDeResultado() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public _linQ_ProjecaoDeResultado(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public _linQ_ProjecaoDeResultado(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public _linQ_ProjecaoDeResultado(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public _linQ_ProjecaoDeResultado(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	[Function(Name="dbo.FxForecastBrasil_CUT", IsComposable=true)]
	public System.Nullable<double> FxForecastBrasil_CUT([Parameter(Name="ContaID", DbType="Int")] System.Nullable<int> contaID, [Parameter(Name="Mes", DbType="Int")] System.Nullable<int> mes, [Parameter(Name="Ano", DbType="Int")] System.Nullable<int> ano, [Parameter(Name="Perc", DbType="Int")] System.Nullable<int> perc)
	{
		return ((System.Nullable<double>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), contaID, mes, ano, perc).ReturnValue));
	}
	
	[Function(Name="dbo.FxForecastBrasil_FAT", IsComposable=true)]
	public System.Nullable<double> FxForecastBrasil_FAT([Parameter(Name="ContaID", DbType="Int")] System.Nullable<int> contaID, [Parameter(Name="Mes", DbType="Int")] System.Nullable<int> mes, [Parameter(Name="Ano", DbType="Int")] System.Nullable<int> ano, [Parameter(Name="Perc", DbType="Int")] System.Nullable<int> perc)
	{
		return ((System.Nullable<double>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), contaID, mes, ano, perc).ReturnValue));
	}
	
	[Function(Name="dbo.FxForecastGrupoContas", IsComposable=true)]
	public System.Nullable<double> FxForecastGrupoContas([Parameter(Name="TipoConta", DbType="Int")] System.Nullable<int> tipoConta, [Parameter(Name="Mes", DbType="Int")] System.Nullable<int> mes, [Parameter(Name="Ano", DbType="Int")] System.Nullable<int> ano, [Parameter(Name="ContratoID", DbType="Int")] System.Nullable<int> contratoID, [Parameter(Name="Perc", DbType="Int")] System.Nullable<int> perc)
	{
		return ((System.Nullable<double>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), tipoConta, mes, ano, contratoID, perc).ReturnValue));
	}
	
	[Function(Name="dbo.FxForecastGrupoContasBrasil_CUT", IsComposable=true)]
	public System.Nullable<double> FxForecastGrupoContasBrasil_CUT([Parameter(Name="TipoConta", DbType="Int")] System.Nullable<int> tipoConta, [Parameter(Name="Mes", DbType="Int")] System.Nullable<int> mes, [Parameter(Name="Ano", DbType="Int")] System.Nullable<int> ano, [Parameter(Name="Perc", DbType="Int")] System.Nullable<int> perc)
	{
		return ((System.Nullable<double>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), tipoConta, mes, ano, perc).ReturnValue));
	}
	
	[Function(Name="dbo.FxForecastGrupoContasBrasil_FAT", IsComposable=true)]
	public System.Nullable<double> FxForecastGrupoContasBrasil_FAT([Parameter(Name="TipoConta", DbType="Int")] System.Nullable<int> tipoConta, [Parameter(Name="Mes", DbType="Int")] System.Nullable<int> mes, [Parameter(Name="Ano", DbType="Int")] System.Nullable<int> ano, [Parameter(Name="Perc", DbType="Int")] System.Nullable<int> perc)
	{
		return ((System.Nullable<double>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), tipoConta, mes, ano, perc).ReturnValue));
	}
	
	[Function(Name="dbo.FxForecast", IsComposable=true)]
	public System.Nullable<double> FxForecast([Parameter(Name="ContaID", DbType="Int")] System.Nullable<int> contaID, [Parameter(Name="Mes", DbType="Int")] System.Nullable<int> mes, [Parameter(Name="Ano", DbType="Int")] System.Nullable<int> ano, [Parameter(Name="ContratoID", DbType="Int")] System.Nullable<int> contratoID, [Parameter(Name="Perc", DbType="Int")] System.Nullable<int> perc)
	{
		return ((System.Nullable<double>)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), contaID, mes, ano, contratoID, perc).ReturnValue));
	}
}
#pragma warning restore 1591

﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
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



[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SGC_NET_V1")]
public partial class _linQ_Prov_Div : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  #endregion
	
	public _linQ_Prov_Div() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["SGC_NET_V1ConnectionString1"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public _linQ_Prov_Div(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public _linQ_Prov_Div(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public _linQ_Prov_Div(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public _linQ_Prov_Div(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.stp_ProvDiv_F_ou_C_Insert")]
	public int stp_ProvDiv_F_ou_C_Insert(
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="FLASHouConsuntivo", DbType="VarChar(50)")] string fLASHouConsuntivo, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="ContaID", DbType="Int")] System.Nullable<int> contaID, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Descricao", DbType="VarChar(200)")] string descricao, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Valor", DbType="VarChar(50)")] string valor, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Mes", DbType="VarChar(50)")] string mes, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Ano", DbType="VarChar(50)")] string ano, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="ContratoID", DbType="Int")] System.Nullable<int> contratoID, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Tipo", DbType="VarChar(50)")] string tipo, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Classificacao", DbType="VarChar(50)")] string classificacao, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(150)")] string login, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="TipoConta", DbType="Int")] System.Nullable<int> tipoConta, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="NormOuRev", DbType="VarChar(50)")] string normOuRev, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="CDebito", DbType="VarChar(50)")] string cDebito, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="CCredito", DbType="VarChar(50)")] string cCredito, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="CC_Princpal", DbType="VarChar(50)")] string cC_Princpal, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="Filial", DbType="Int")] System.Nullable<int> filial, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="RAT", DbType="NChar(1)")] System.Nullable<char> rAT, 
				[global::System.Data.Linq.Mapping.ParameterAttribute(Name="BU", DbType="VarChar(50)")] string bU)
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), fLASHouConsuntivo, contaID, descricao, valor, mes, ano, contratoID, tipo, classificacao, login, tipoConta, normOuRev, cDebito, cCredito, cC_Princpal, filial, rAT, bU);
		return ((int)(result.ReturnValue));
	}
}
#pragma warning restore 1591

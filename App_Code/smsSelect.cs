using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App_Code;

public class smsSelect
{
    
    Persistencia_Fast SMS = new Persistencia_Fast();
    readonly cSession SgcSess = new cSession();


    public void smsInsert(string assunto, string texto, string UserID_Destinatario, string status)
    {        
        SMS.atualizaInsereDados(@"INSERT INTO [tbl_sms_myHome] 
                                   ([Assunto]
                                   ,[Texto]
                                   ,[Remetente]
                                   ,[ID_Destinatario]
                                   ,[Tipo]
                                   ,[Status]
                                   ,[lida]
                                   ,[Data])
                             VALUES
                                   ('" + assunto + @"'
                                   ,'" + texto + @"'
                                   ,'" +SgcSess.FullName + @"'
                                   ,"+ UserID_Destinatario + @"
                                   ,'sms'
                                   ,'Alta'
                                   ,'nlida'
                                   ,Convert(DateTime, GetDate(), 111))");
    }
    public void smsLida(string ID)
    {        
        SMS.atualizaInsereDados("UPDATE [tbl_sms_myHome] SET [lida] = 'lida' WHERE ID = " + ID + " and [ID_Destinatario] <> 0");
    }

    public void smsDel(string ID)
    {
        SMS.atualizaInsereDados("DELETE FROM [tbl_sms_myHome] WHERE ID = " + ID + " and [ID_Destinatario] <> 0");
    }
}
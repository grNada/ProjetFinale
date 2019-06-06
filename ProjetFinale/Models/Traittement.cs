using ProjetFinale.Controllers;
using ProjetFinale.Models;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetFinale.Models
{
    public class Traittement
    {
        public object Condidat { get; internal set; }
        #region parametre

        // string erreur = "";
        #endregion
        // Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public string chaine_connexion_Traitement()
        {

            string connexion = "Data Source=HPPORTABLE-PC\\PFE;Initial Catalog=FREELANCE;Integrated Security=True";

            string ConnectBT = connexion;

            return ConnectBT;
        }

       
        #region type
        public static int? retournIntnull(object value)
        {
            int ouput = 0;
            if ((value != null) && int.TryParse(value.ToString(), out ouput))
            {
                ouput = int.Parse(value.ToString());
                return ouput;
            }
            else
                return null;

        }


        public static int retournInt(object value)
        {
            int ouput = 0;
            if ((value != null) && int.TryParse(value.ToString(), out ouput))
            {
                ouput = int.Parse(value.ToString());
            }
            return ouput;
        }

        public static decimal retourndecimal(object value)
        {
            decimal ouput = 0;
            double v = 0;
            string p = value.ToString();
            bool t = double.TryParse(value.ToString(), out v);
            if ((value != null) && decimal.TryParse(value.ToString(), out ouput))
            {
                ouput = decimal.Parse(value.ToString());
            }
            return ouput;
        }

        internal List<Controllers.CondidatController> GetCand()
        {
            throw new NotImplementedException();
        }

        public static bool retournbool(object value)
        {
            bool ouput = false;
            if ((value != null) && bool.TryParse(value.ToString(), out ouput))
            {
                ouput = bool.Parse(value.ToString());
            }
            return ouput;
        }
        public static DateTime retourndate(object value)
        {
            DateTime ouput = new DateTime();
            if ((value != null) && DateTime.TryParse(value.ToString(), out ouput))
            {
                ouput = DateTime.Parse(value.ToString());
            }
            return ouput;
        }

        public int retournIntI(string value)
        {
            int ouput = 0;
            if ((value != null) && int.TryParse(value.ToString(), out ouput))
            {
                ouput = int.Parse(value.ToString());
            }
            return ouput;
        }

        public decimal retourndecimalI(string value)
        {
            decimal ouput = 0;

            if ((value.Replace(",", ".") != null) && decimal.TryParse(value.Replace(",", ".").ToString(), out ouput))
            {
                ouput = decimal.Parse(value.Replace(",", ".").ToString());
            }
            return ouput;
        }
        public bool retournboolI(string value)
        {
            bool ouput = false;
            if ((value != null) && bool.TryParse(value.ToString(), out ouput))
            {
                ouput = bool.Parse(value.ToString());
            }

            return ouput;
        }
        public DateTime retourndateI(string value)
        {
            DateTime ouput = Convert.ToDateTime("1900-01-01"); ;
            if ((value != null) && DateTime.TryParse(value.ToString(), out ouput))
            {
                ouput = DateTime.Parse(value.ToString());
            }
            return ouput;
        }
        public DateTime retourndateexcel(string value)
        {
            DateTime ouput = Convert.ToDateTime("1900-01-01"); ;
            if ((value != null) && DateTime.TryParse(value.ToString(), out ouput))
            {
                ouput = DateTime.Parse(value.ToString());
            }
            else
            {

                double d = double.Parse(value);
                ouput = DateTime.FromOADate(d);

            }

            return ouput;
        }

        #endregion type

        #region gestion activiter
        public DataSet ListActiviter()
        {
            string Connect = chaine_connexion_Traitement();

            ConnexionDB.MaConn = new SqlConnection(Connect);

            string query = " SELECT       * FROM            Activiter";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public string GetPasswordActiviter(int code)
        {

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT   id_job  FROM Activiter where id_activiter = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string id_job = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                id_job = ds.Tables[0].Rows[i]["id_job"].ToString();
            }
            return id_job;


        }
        public Activiter GetActiviter(int code)
        {
            Activiter lignes = new Activiter();

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT *FROM Activiter where id_activiter = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lignes.id_activiter = retournInt(ds.Tables[0].Rows[i]["id_activiter"].ToString());

                lignes.id_job = int.Parse(ds.Tables[0].Rows[i]["id_job"].ToString());
                lignes.id_personne = int.Parse(ds.Tables[0].Rows[i]["id_personne"].ToString());
                lignes.datePostulation = ds.Tables[0].Rows[i]["datePostulation"].ToString();
                lignes.email_postuler = ds.Tables[0].Rows[i]["email_postuler"].ToString();


            }
            return lignes;

        }
        public bool ajout_Activiter(ref string erreur, Activiter act)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();
            if (act.id_activiter == 0)
            {
                cmd.CommandText = "INSERT INTO [dbo].[Activiter]([id_activiter],[id_job],[id_personne],[datePostulation],[email_postuler])VALUES('" + act.id_activiter + "','" + act.id_job + "','" + act.id_personne + "','" + act.datePostulation+"','" + act.email_postuler + "')";
                listescmd.Add(cmd);
            }
            else
            {
                cmd.CommandText = "UPDATE       Activiter   SET [datePostulation] ='" + act.datePostulation + "'"
      + ",[id_job] = '" + act.id_job + "'"
      + ",[id_personne] = '" + act.id_personne + "'" + " ,[email_postuler] = '" + act.email_postuler 
     + " ,[datePostulation] = '" + act.datePostulation + "'  where  ([id_activiter]=" + act.id_activiter + ") ";
                listescmd.Add(cmd);
                cmd = new SqlCommand();

            }
            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool modifier_Compte(ref string erreur, Activiter act)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();


            cmd.CommandText = "UPDATE       Activiter   SET [datePostulation] ='" + act.datePostulation + "'"

      + ",[id_job] = '" + act.id_job + "'"
      + ",[id_personne] = '" + act.id_personne + "'" + ",[datePostulation] ='" + act.datePostulation + "'"
      + " ,[email_postuler] = '" + act.email_postuler + "'  where  ([id_activiter]=" + act.id_activiter + ") ";
            listescmd.Add(cmd);
            cmd = new SqlCommand();



            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool delete_Activiter(ref string erreur, int id)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string sql = "DELETE FROM  [Activiter]  WHERE [id_activiter]=" + id + "";
            bool ins = ConnexionDB.ExecuteSql(ref erreur, sql, ConnexionDB.MaConn);
            return ins;
        }
        public List<Activiter> ListeActiviters()
        {
            List<Activiter> liste = new List<Activiter>();
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string query =                   " SELECT * FROM            Activiter ";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Activiter lignes = new Activiter();


                lignes.id_activiter = retournInt(ds.Tables[0].Rows[i]["id_activiter"].ToString());

                lignes.id_job = int.Parse(ds.Tables[0].Rows[i]["id_job"].ToString());
                lignes.id_personne = int.Parse(ds.Tables[0].Rows[i]["id_personne"].ToString());
                lignes.datePostulation = ds.Tables[0].Rows[i]["datePostulation"].ToString();
                lignes.email_postuler = ds.Tables[0].Rows[i]["email_postuler"].ToString();
                liste.Add(lignes);
            }
            return liste;

        }
        #endregion







        #region gestion utilisateur

        public DataSet ListUtilisateur()
        {
            string Connect = chaine_connexion_Traitement();

            ConnexionDB.MaConn = new SqlConnection(Connect);

            string query = " SELECT       * FROM            Utilisateur";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public string GetPasswordUtilisateur(int code)
        {

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT   password  FROM Utilisateur where id_user = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string password = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                password = ds.Tables[0].Rows[i]["password"].ToString();
            }
            return password;


        }
        public Utilisateur GetUtilisateur(int code)
        {
            Utilisateur lignes = new Utilisateur();

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT *FROM Utilisateur where id_user = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lignes.id_user = retournInt(ds.Tables[0].Rows[i]["id_user"].ToString());
                lignes.gouvernorat = ds.Tables[0].Rows[i]["gouvernorat"].ToString();
                lignes.email = ds.Tables[0].Rows[i]["email"].ToString();
                lignes.tel = ds.Tables[0].Rows[i]["tel"].ToString();
                lignes.password = ds.Tables[0].Rows[i]["password"].ToString();
                lignes.datenaissance = ds.Tables[0].Rows[i]["datenaissance"].ToString();
                lignes.sexe = ds.Tables[0].Rows[i]["sexe"].ToString();


            }
            return lignes;

        }
        public bool ajout_Utilisateur(ref string erreur, Utilisateur user)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();
            if (user.id_user == 0)
            {
                cmd.CommandText = "INSERT INTO [dbo].[Utilisateur]([gouvernorat],[email],[tel],[password],[datenaissance],[sexe])VALUES('" + user.gouvernorat + "','" + user.email + "','" + user.tel  + "','" + user.password + "','" + user.datenaissance + "','" + user.sexe + "')";
                listescmd.Add(cmd);
            }
            else
            {
                cmd.CommandText = "UPDATE       Utilisateur   SET [gouvernorat] ='" + user.gouvernorat + "'"
     + " ,[email] = '" + user.email + "'"
      + ",[tel] = '" + user.tel + "'"
     + ",[password] = '" + user.password + "'"
     + ",[datenaissance] = '" + user.datenaissance + "'"
       + " ,[sexe] = '" + user.sexe + "'  where ([id_user]=" + user.id_user + ") ";
                listescmd.Add(cmd);
                cmd = new SqlCommand();

            }
            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool modifier_Compte(ref string erreur, Utilisateur user)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();


            cmd.CommandText = "UPDATE       Utilisateur   SET [gouvernorat] ='" + user.gouvernorat + "'"
    + " ,[email] = '" + user.email + "'"
     + ",[tel] = '" + user.tel + "'"
     + ",[password] = '" + user.password + "'"
     + ",[datenaissance] = '" + user.datenaissance + "'"
       + " ,[sexe] = '" + user.sexe + "'  where ([id_user]=" + user.id_user + ") ";
            cmd = new SqlCommand();




            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool delete_Utilisateur(ref string erreur, int id)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string sql = "DELETE FROM  [Utilisateur]  WHERE [id_user]=" + id + "";
            bool ins = ConnexionDB.ExecuteSql(ref erreur, sql, ConnexionDB.MaConn);
            return ins;
        }
        public List<Utilisateur> ListeUtilisateurs()
        {
            List<Utilisateur> liste = new List<Utilisateur>();
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string query =" SELECT * FROM Utilisateur ";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Utilisateur lignes = new Utilisateur();


                lignes.id_user = retournInt(ds.Tables[0].Rows[i]["id_user"].ToString());

                lignes.gouvernorat = (ds.Tables[0].Rows[i]["gouvernorat"].ToString());
                lignes.email = (ds.Tables[0].Rows[i]["email"].ToString());
                lignes.tel = (ds.Tables[0].Rows[i]["tel"].ToString());
                lignes.password = (ds.Tables[0].Rows[i]["password"].ToString());
                lignes.datenaissance = (ds.Tables[0].Rows[i]["datenaissance"].ToString());
                lignes.sexe = (ds.Tables[0].Rows[i]["sexe"].ToString());
                liste.Add(lignes);
            }
            return liste;

        }
        





        #endregion  type
        #region gestion Condidat
        public DataSet ListCondidat()
        {
            string Connect = chaine_connexion_Traitement();

            ConnexionDB.MaConn = new SqlConnection(Connect);

            string query = " SELECT       * FROM            Condidat";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public List<string> GetEmail ()
        {
            List<string> listemail = new List<string>();
            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT   email  FROM Condidat ";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string mail = "";

                    mail = (ds.Tables[0].Rows[i]["email"].ToString());
                    listemail.Add(mail);
                }

                return listemail;

            }else
            {
                return null;
            }
        }
        public Condidat GetCand(string username)
        {
            Condidat lignes = new Condidat();

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT *FROM Condidat where username = " + username + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lignes.id_personne = retournInt(ds.Tables[0].Rows[i]["id_personne"].ToString());
                lignes.id_user = int.Parse(ds.Tables[0].Rows[i]["id_user"].ToString());
                lignes.email = ds.Tables[0].Rows[i]["email"].ToString();
                lignes.password = ds.Tables[0].Rows[i]["password"].ToString();
                lignes.username = ds.Tables[0].Rows[i]["username"].ToString();
                lignes.cv = ds.Tables[0].Rows[i]["cv"].ToString();
                lignes.gouvernorat = ds.Tables[0].Rows[i]["gouvernorat"].ToString();
                lignes.tel = int.Parse(ds.Tables[0].Rows[i]["tel"].ToString());
                lignes.sexe = ds.Tables[0].Rows[i]["sexe"].ToString();
                lignes.id_competence = int.Parse(ds.Tables[0].Rows[i]["id_competence"].ToString());
                lignes.competence = ds.Tables[0].Rows[i]["competence"].ToString();
                lignes.datenaissance = ds.Tables[0].Rows[i]["datenaissance"].ToString();




            }
            return lignes;

        }

        public string GetPasswordUser(int code)
        {

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT   nom  FROM Condidat where id_personne = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string nom = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                nom = (ds.Tables[0].Rows[i]["nom"].ToString());
            }
            return nom;


        }
        public Condidat GetCandidat(int code)
        {
            Condidat lignes = new Condidat();

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT *FROM Condidat where id_personne = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lignes.id_personne = retournInt(ds.Tables[0].Rows[i]["id_personne"].ToString());
                lignes.id_user = int.Parse(ds.Tables[0].Rows[i]["id_user"].ToString());
                lignes.email = ds.Tables[0].Rows[i]["email"].ToString();
                lignes.password = ds.Tables[0].Rows[i]["password"].ToString();
                lignes.username = ds.Tables[0].Rows[i]["username"].ToString();
                lignes.cv = ds.Tables[0].Rows[i]["cv"].ToString();
                lignes.gouvernorat = ds.Tables[0].Rows[i]["gouvernorat"].ToString();
                lignes.tel = int.Parse(ds.Tables[0].Rows[i]["tel"].ToString());
                lignes.sexe = ds.Tables[0].Rows[i]["sexe"].ToString();
                lignes.id_competence = int.Parse(ds.Tables[0].Rows[i]["id_competence"].ToString());
                lignes.competence = ds.Tables[0].Rows[i]["competence"].ToString();
                lignes.datenaissance = ds.Tables[0].Rows[i]["datenaissance"].ToString();




            }
            return lignes;

        }

        public bool ajout_Candidat(ref string erreur, Condidat pers)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();
            if (pers.id_personne == 0)
            {
                cmd.CommandText = "INSERT INTO [dbo].[Condidat]([id_personne],[id_user],[username],[email],[password],[cv],[gouvernorat],[tel],[sexe],[id_competence],[competence],[datenaissance])VALUES('" + pers.id_personne + "','" + pers.id_user + "','" +pers.username+ "','" + pers.email + "','" + pers.password + "','" + pers.cv + "','" + pers.gouvernorat + "','" + pers.tel + "','" + pers.sexe + "','" + pers.id_competence + "','" + pers.competence + "','" + pers.datenaissance + "')";
                listescmd.Add(cmd);
            }
            else
            {
                cmd.CommandText = "UPDATE       Condidat   SET [id_user] = '" + pers.username + "'"
                   + ",[username] = '" + pers.username + "'"
                   + ",[email] = '" + pers.email + "'"
                   + ",[password] = '" + pers.password + "'"
                   + ",[cv]" + pers.cv + "'"
                    + ",[gouvernorat] = '" + pers.gouvernorat + "'"
                   + ",[tel]" + pers.tel + "'"
                   + ",[sexe]" + pers.sexe + "'"
                   + ",[id_competence] = '" + pers.id_competence + "'"
                   + ",[cometence] = '" + pers.competence
                   + ",[datenaissance] = '" + pers.datenaissance + "'" 
                   + "' where ([id_personne]=" + pers.id_personne + ") ";
                listescmd.Add(cmd);
                cmd = new SqlCommand();

            }
            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool modifier_Compte(ref string erreur, Condidat pers)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();
            cmd.CommandText = "UPDATE       Condidat   SET [id_user] = '" + pers.username + "'"
                   + ",[username] = '" + pers.username + "'"
                   + ",[email] = '" + pers.email + "'"
                   + ",[password] = '" + pers.password + "'"
                   + ",[cv]" + pers.cv + "'"
                    + ",[gouvernorat] = '" + pers.gouvernorat + "'"
                   + ",[tel]" + pers.tel + "'"
                   + ",[sexe]" + pers.sexe + "'"
                   + ",[id_competence] = '" + pers.id_competence + "'"
                   + ",[cometence] = '" + pers.competence
                    + ",[datenaissance] = '" + pers.datenaissance + "'" 
                    + "' where ([id_personne]=" + pers.id_personne + ") ";

          
            listescmd.Add(cmd);
            cmd = new SqlCommand();
            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool delete_Candidat(ref string erreur, int id)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string sql = "DELETE FROM  [Condidat]  WHERE [id_pers]=" + id + "";
            bool ins = ConnexionDB.ExecuteSql(ref erreur, sql, ConnexionDB.MaConn);
            return ins;
        }
        public List<Condidat> ListeCondidats()
        {
            List<Condidat> liste = new List<Condidat>();
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string query = " SELECT * FROM            Condidat ";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Condidat lignes = new Condidat();

                lignes.id_personne = retournInt(ds.Tables[0].Rows[i]["id_personne"].ToString());
                lignes.id_user = retournInt(ds.Tables[0].Rows[i]["id_user"].ToString());
                lignes.email = ds.Tables[0].Rows[i]["email"].ToString();
                lignes.password = ds.Tables[0].Rows[i]["password"].ToString();
                lignes.username = ds.Tables[0].Rows[i]["username"].ToString();
                lignes.cv = ds.Tables[0].Rows[i]["cv"].ToString();
                lignes.gouvernorat = ds.Tables[0].Rows[i]["gouvernorat"].ToString();
                lignes.tel = retournInt(ds.Tables[0].Rows[i]["tel"].ToString());
                lignes.sexe = ds.Tables[0].Rows[i]["sexe"].ToString();
                lignes.id_competence = retournInt(ds.Tables[0].Rows[i]["id_competence"].ToString());
                lignes.competence = ds.Tables[0].Rows[i]["competence"].ToString();
                lignes.datenaissance = ds.Tables[0].Rows[i]["datenaissance"].ToString();

                liste.Add(lignes);
            }
            return liste;

        }
        #endregion


        #region gestion Competence
        public DataSet ListCompetence()
        {
            string Connect = chaine_connexion_Traitement();

            ConnexionDB.MaConn = new SqlConnection(Connect);

            string query = " SELECT       * FROM            Competence";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public string GetPasswordCompetence(int code)
        {

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT   nom  FROM Competence where id_competence = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string nom = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                nom = (ds.Tables[0].Rows[i]["nom"].ToString());
            }
            return nom;


        }
        public Competence GetCompetence(int code)
        {
            Competence lignes = new Competence();

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT *FROM Competence where id_competence = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lignes.id_competence = retournInt(ds.Tables[0].Rows[i]["id_competence"].ToString());
                lignes.id_personne = retournInt(ds.Tables[0].Rows[i]["id_personne"].ToString());

                lignes.nom = ds.Tables[0].Rows[i]["nom"].ToString();



            }
            return lignes;

        }
        public bool ajout_Competence(ref string erreur, Competence cpt)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();
            if (cpt.id_personne == 0)
            {
                cmd.CommandText = "INSERT INTO [dbo].[Competence]([id_competence],[nom],[id_personne])VALUES('" + cpt.id_competence + "','" + cpt.nom + "','" + cpt.id_personne + "')";
                listescmd.Add(cmd);
            }
            else
            {
                cmd.CommandText = "UPDATE       Competence   SET [nom] ='" + cpt.nom + "'"
     + " ,[nom] = '" + cpt.nom + "'"
      + " ,[id_personne] = '" + cpt.id_personne + "'  where ([id_competence]=" + cpt.id_competence + ") ";
                listescmd.Add(cmd);
                cmd = new SqlCommand();

            }
            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool modifier_Competence(ref string erreur, Competence cpt)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();


            cmd.CommandText = "UPDATE       Competence   SET [nom] ='" + cpt.nom + "'"
     + " ,[nom] = '" + cpt.nom + "'"
      + " ,[id_personne] = '" + cpt.id_personne + "'  where ([id_competence]=" + cpt.id_competence + ") ";
            listescmd.Add(cmd);
            cmd = new SqlCommand();



            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool delete_Competence(ref string erreur, int id)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string sql = "DELETE FROM  [Competence]  WHERE [id_competence]=" + id + "";
            bool ins = ConnexionDB.ExecuteSql(ref erreur, sql, ConnexionDB.MaConn);
            return ins;
        }
        public List<Competence> ListeCompetences()
        {
            List<Competence> liste = new List<Competence>();
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string query =                   " SELECT * FROM            Competence ";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Competence lignes = new Competence();


                lignes.id_competence = retournInt(ds.Tables[0].Rows[i]["id_competence"].ToString());

                lignes.nom = (ds.Tables[0].Rows[i]["nom"].ToString());
                lignes.id_personne = retournInt(ds.Tables[0].Rows[i]["id_personne"].ToString());


                liste.Add(lignes);
            }
            return liste;

        }
        #endregion
        #region gestion Compte
        public DataSet ListCompte()
        {
            string Connect = chaine_connexion_Traitement();

            ConnexionDB.MaConn = new SqlConnection(Connect);

            string query = " SELECT       * FROM            Compte";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public string GetPasswordCompte(int code)
        {

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT   password  FROM Compte where id_compte = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string password = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                password = (ds.Tables[0].Rows[i]["password"].ToString());
            }
            return password;


        }
        public Compte GetCompte(int code)
        {
            Compte lignes = new Compte();

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT *FROM Compte where id_compte = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lignes.id_Compte = retournInt(ds.Tables[0].Rows[i]["id_Compte"].ToString());

                lignes.id_user = retournInt(ds.Tables[0].Rows[i]["id_user"].ToString());
                lignes.password = ds.Tables[0].Rows[i]["password"].ToString();
                lignes.email = ds.Tables[0].Rows[i]["mail"].ToString();

            }
            return lignes;

        }
        public bool ajout_Compte(ref string erreur, Compte cmp)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();
            if (cmp.id_Compte == 0)
            {
                cmd.CommandText = "INSERT INTO [dbo].[Compte]([id_Compte],[id_user],[password],[email])VALUES('" + cmp.id_Compte + "','" + cmp.id_user + "','" + cmp.email + "','" + cmp.password + "')";
                listescmd.Add(cmd);
            }
            else
            {
                cmd.CommandText = "UPDATE       Compte   SET [email] ='" + cmp.email + "'"
      + ",[id_user] = '" + cmp.id_user + "'"

     + " ,[password] = '" + cmp.password + "'  where ([id_Compte]=" + cmp.id_Compte + ") ";
                listescmd.Add(cmd);
                cmd = new SqlCommand();

            }
            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool modifier_Comptes(ref string erreur, Compte cmp)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();


            cmd.CommandText = "UPDATE      Compte   SET [email] ='" + cmp.email + "'"
      + ",[id_user] = '" + cmp.id_user + "'"

     + " ,[password] = '" + cmp.password + "'  where ([id_Compte]=" + cmp.id_Compte + ") ";
            listescmd.Add(cmd);
            cmd = new SqlCommand();




            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool delete_Compte(ref string erreur, int id)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string sql = "DELETE FROM  [Compte]  WHERE [id_compte]=" + id + "";
            bool ins = ConnexionDB.ExecuteSql(ref erreur, sql, ConnexionDB.MaConn);
            return ins;
        }
        public List<Compte> ListeComptes()
        {
            List<Compte> liste = new List<Compte>();
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string query =                   " SELECT * FROM            Compte ";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Compte lignes = new Compte();


                lignes.id_Compte = retournInt(ds.Tables[0].Rows[i]["id_Compte"].ToString());

                lignes.id_user = retournInt(ds.Tables[0].Rows[i]["id_user"].ToString());
                lignes.password = ds.Tables[0].Rows[i]["password"].ToString();
                lignes.email = ds.Tables[0].Rows[i]["mail"].ToString();
                liste.Add(lignes);
            }
            return liste;

        }
        #endregion
        #region gestion enterprise
        public DataSet ListUser()
        {
            string Connect = chaine_connexion_Traitement();

            ConnexionDB.MaConn = new SqlConnection(Connect);

            string query = " SELECT       * FROM            Entreprise";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public string GetPasswordEntreprise(int code)
        {

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT   nom  FROM Entreprise where id_entreprise = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string nom = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                nom = (ds.Tables[0].Rows[i]["nom"].ToString());
            }
            return nom;


        }
        public Entreprise GetEntreprise(int code)
        {
            Entreprise lignes = new Entreprise();

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT *FROM Entreprise where id_entreprise = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lignes.id_entreprise = retournInt(ds.Tables[0].Rows[i]["id_entreprise"].ToString());
                lignes.id_user = retournInt(ds.Tables[0].Rows[i]["id_user"].ToString());
                lignes.email = ds.Tables[0].Rows[i]["email"].ToString();
                lignes.password = ds.Tables[0].Rows[i]["password"].ToString();
                lignes.nom_entreprise = ds.Tables[0].Rows[i]["nom_entreprise"].ToString();
                lignes.gouvernorat = ds.Tables[0].Rows[i]["gouvernorat"].ToString();
                lignes.tel = retournInt(ds.Tables[0].Rows[i]["tel"].ToString());
                lignes.sexe = ds.Tables[0].Rows[i]["sexe"].ToString();
                lignes.id_activité = retournInt(ds.Tables[0].Rows[i]["id_activité"].ToString());
                lignes.activite = ds.Tables[0].Rows[i]["activite"].ToString();
                lignes.datenaissance = ds.Tables[0].Rows[i]["datenaissance"].ToString();


            }
            return lignes;

        }
        public bool ajout_Entreprise(ref string erreur, Entreprise entrp)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();
            if (entrp.id_entreprise == 0)
            {
                cmd.CommandText = "INSERT INTO [dbo].[Entreprise]([id_entreprise],[id_user],[email],[password],[nom_entreprise],[gouvernorat],[tel],[sexe],[id_activité],[activite],[datenaissance])VALUES('" + entrp.id_entreprise  + "','" + entrp.id_user + "','" + entrp.email + "','" +entrp.password + "','" +entrp.nom_entreprise + "','" + entrp.gouvernorat + "','" + entrp.tel + "','" + entrp.sexe + "','" + entrp.id_activité + "','" + entrp.activite + "','" + entrp.datenaissance + "')";
                listescmd.Add(cmd);
            }
            else
            {
                cmd.CommandText = "UPDATE       Entreprise   SET [id_user] ='" + entrp.id_user + "'"
              + " ,[email] = '" + entrp.email
              + " ,[password] = '" + entrp.password
              + " ,[nom_entreprise] = '" + entrp.nom_entreprise
              + " ,[gouvernorat] = '" + entrp.gouvernorat
              + " ,[tel] = '" + entrp.tel
              + " ,[sexe] = '" + entrp.sexe
              + " ,[id_activité] = '" + entrp.id_activité
              + " ,[activite] = '" + entrp.activite
              + " ,[datenaissance] = '" + entrp.datenaissance
              + "'  where ([id_entreprise]=" + entrp.id_entreprise + ") ";
                listescmd.Add(cmd);
                cmd = new SqlCommand();

            }
            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool modifier_Compte(ref string erreur, Entreprise entrp)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();

            cmd.CommandText = "UPDATE       Entreprise   SET [id_user] ='" + entrp.id_user + "'"

                        + " ,[email] = '" + entrp.email
                        + " ,[password] = '" + entrp.password
                        + " ,[nom_entreprise] = '" + entrp.nom_entreprise
                        + " ,[gouvernorat] = '" + entrp.gouvernorat
                        + " ,[tel] = '" + entrp.tel
                        + " ,[sexe] = '" + entrp.sexe
                        + " ,[id_activité] = '" + entrp.id_activité
                        + " ,[activité] = '" + entrp.activite
                        + " ,[datenaissance] = '" + entrp.datenaissance
                        + "'  where ([id_entreprise]=" + entrp.id_entreprise + ") ";
            listescmd.Add(cmd);
            cmd = new SqlCommand();
            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool delete_Entreprise(ref string erreur, int id)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string sql = "DELETE FROM  [Entreprise]  WHERE [id_user]=" + id + "";
            bool ins = ConnexionDB.ExecuteSql(ref erreur, sql, ConnexionDB.MaConn);
            return ins;
        }
        public List<Entreprise> ListeEntreprises()
        {
            List<Entreprise> liste = new List<Entreprise>();
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string query  = " SELECT * FROM            Entreprise ";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Entreprise lignes = new Entreprise();
                lignes.id_entreprise = retournInt(ds.Tables[0].Rows[i]["id_entreprise"].ToString());
                lignes.id_user = retournInt(ds.Tables[0].Rows[i]["id_user"].ToString());
                lignes.email = ds.Tables[0].Rows[i]["email"].ToString();
                lignes.password = ds.Tables[0].Rows[i]["password"].ToString();
                lignes.nom_entreprise = ds.Tables[0].Rows[i]["nom_entreprise"].ToString();
                lignes.gouvernorat = ds.Tables[0].Rows[i]["gouvernorat"].ToString();
                lignes.tel = retournInt(ds.Tables[0].Rows[i]["tel"].ToString());
                lignes.sexe = ds.Tables[0].Rows[i]["sexe"].ToString();
                lignes.id_activité = retournInt(ds.Tables[0].Rows[i]["id_activité"].ToString());
                lignes.activite = ds.Tables[0].Rows[i]["activite"].ToString(); 
                lignes.datenaissance = ds.Tables[0].Rows[i]["datenaissance"].ToString();
                liste.Add(lignes);
            }
            return liste;

        }

        #endregion
        #region gestion Job
        public DataSet ListJob()
        {
            string Connect = chaine_connexion_Traitement();

            ConnexionDB.MaConn = new SqlConnection(Connect);

            string query = " SELECT       * FROM            Job";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public string GetPasswordJob(int code)
        {
           
            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT   password  FROM Job where id_job = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string password = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                password = (ds.Tables[0].Rows[i]["password"].ToString());
            }
            return password;


        }
        public Job GetJob(int code)
        {
            Job lignes = new Job();

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT *FROM Job where id_job = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lignes.id_job = retournInt(ds.Tables[0].Rows[i]["id_job"].ToString());

                lignes.id_user = retournInt(ds.Tables[0].Rows[i]["id_user"].ToString());
                lignes.date = ds.Tables[0].Rows[i]["date"].ToString();
                lignes.title = ds.Tables[0].Rows[i]["title"].ToString();
                lignes.description = ds.Tables[0].Rows[i]["description"].ToString();
                lignes.name_annonceur = ds.Tables[0].Rows[i]["name_annonceur"].ToString();
                lignes.phone = ds.Tables[0].Rows[i]["phone"].ToString();
                lignes.email = ds.Tables[0].Rows[i]["email"].ToString();
                lignes.budgee = ds.Tables[0].Rows[i]["budgee"].ToString();
                lignes.duree = ds.Tables[0].Rows[i]["duree"].ToString();

            }
            return lignes;

        }
        public bool ajout_Job(ref string erreur, Job j)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();
            if (j.id_job == 0)
            {
                cmd.CommandText = "INSERT INTO [dbo].[Job]([id_job],[id_user],[date],[title],[description],[name_annonceur],[phone],[email],[budgee],[duree])VALUES('" + j.id_job + "','" + j.id_user + "','" + j.date + "','" + j.title + "','" + j.description  + "','" + j.name_annonceur + "','" + j.phone + "','" + j.email + "','" + j.budgee+ "','" + j.duree + "' )";
                listescmd.Add(cmd);
            }
            else
            {
                cmd.CommandText = "UPDATE       Job   SET [id_user] ='" + j.id_user + "'" +
                    ",[date]= '" + j.date +
                    ",[title]= '" + j.title +
                    ",[description]= '" + j.description +
                    ",[name_annonceur]= '" + j.name_annonceur +
                    ",[phone]= '" + j.phone +
                    ",[email] = '" + j.email+
                    ",[budgee] = '" + j.budgee+
                    ",[duree] = '" + j.duree
                    + "'  where ([id_job]=" + j.id_job + ") ";
                listescmd.Add(cmd);
                cmd = new SqlCommand();

            }
            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool modifier_Compte(ref string erreur, Job j)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();
            cmd.CommandText = "UPDATE       Job   SET [id_user] ='" + j.id_user + "'" +
                   ",[date]= '" + j.date +
                   ",[title]= '" + j.title +
                   ",[description]= '" + j.description +
                   ",[name_annonceur]= '" + j.name_annonceur +
                   ",[phone]= '" + j.phone +
                   ",[email] = '" + j.email+
                    ",[budgee] = '" + j.duree+
                     ",[budgee] = '" + j.duree
                   + "'  where ([id_job]=" + j.id_job + ") ";
            listescmd.Add(cmd);
            cmd = new SqlCommand();
            listescmd.Add(cmd);
            cmd = new SqlCommand();
            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool delete_Job(ref string erreur, int id)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string sql = "DELETE FROM  [Job]  WHERE [id_job]=" + id + "";
            bool ins = ConnexionDB.ExecuteSql(ref erreur, sql, ConnexionDB.MaConn);
            return ins;
        }
        public List<Job> ListeJobs()
        {
            List<Job> liste = new List<Job>();
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string query =" SELECT * FROM            Job ";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Job lignes = new Job();


                lignes.id_user = retournInt(ds.Tables[0].Rows[i]["id_user"].ToString());

                lignes.id_job = retournInt(ds.Tables[0].Rows[i]["id_job"].ToString());
                lignes.date =  ds.Tables[0].Rows[i]["date"].ToString();
                lignes.title = ds.Tables[0].Rows[i]["title"].ToString();
                lignes.description = ds.Tables[0].Rows[i]["description"].ToString();
                lignes.name_annonceur = ds.Tables[0].Rows[i]["name_annonceur"].ToString();
                lignes.phone = ds.Tables[0].Rows[i]["phone"].ToString();
                lignes.email = ds.Tables[0].Rows[i]["email"].ToString();
                lignes.budgee = ds.Tables[0].Rows[i]["budgee"].ToString();
                lignes.duree = ds.Tables[0].Rows[i]["duree"].ToString();

                liste.Add(lignes);
            }
            return liste;

        }

        #endregion



        #region gestion Message
        public DataSet ListMessage()
        {
            string Connect = chaine_connexion_Traitement();

            ConnexionDB.MaConn = new SqlConnection(Connect);

            string query = " SELECT       * FROM            Message";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public string GetPasswordMessage(int code)
        {

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT   nomMembre  FROM Message where id_message= " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string nomMembre = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                nomMembre = (ds.Tables[0].Rows[i]["nomMembre"].ToString());
            }
            return nomMembre;


        }
        public Message GetMessage(int code)
        {
            Message lignes = new Message();

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT *FROM Message where id_message = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lignes.id_message = retournInt(ds.Tables[0].Rows[i]["id_message"].ToString());

                lignes.id_user = retournInt(ds.Tables[0].Rows[i]["id_user"].ToString());
                lignes.nomMembre = ds.Tables[0].Rows[i]["nomMembre"].ToString();
                lignes.sujet = ds.Tables[0].Rows[i]["sujet"].ToString();
                lignes.description = ds.Tables[0].Rows[i]["description"].ToString();
                lignes.emetteur = ds.Tables[0].Rows[i]["emetteur"].ToString();
                lignes.recepteur = ds.Tables[0].Rows[i]["recepteur"].ToString();


            }
            return lignes;

        }
        public bool ajout_Message(ref string erreur, Message msg)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();
            if (msg.id_message == 0)
            {
                cmd.CommandText = "INSERT INTO [dbo].[Message]([id_message],[id_user],[nomMembre],[sujet],[description],[emetteur],[recepteur])VALUES('" + msg.id_message + "','" + msg.id_user + "','" + msg.nomMembre + "','" + msg.sujet + "','" + msg.description + "','" + msg.emetteur + "','" + msg.recepteur + "')";
                listescmd.Add(cmd);
            }
            else
            {
                cmd.CommandText = "UPDATE       Message   SET [nomMembre] ='" + msg.nomMembre + "'"
     + " ,[id_user] = '" + msg.id_user + "'"
      + ",[sujet] = '" + msg.sujet + "'"
      + ",[description] = '" + msg.description + "'"
      + ",[emetteur] = '" + msg.emetteur + "'"

     + " ,[recepteur] = '" + msg.recepteur + "'  where ([id_message]=" + msg.id_message + ") ";
                listescmd.Add(cmd);
                cmd = new SqlCommand();

            }
            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool modifier_Compte(ref string erreur, Message msg)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();


            cmd.CommandText = "UPDATE       Message   SET [nomMembre] ='" + msg.nomMembre + "'"
     + " ,[id_user] = '" + msg.id_user + "'"
      + ",[sujet] = '" + msg.sujet + "'"
      + ",[description] = '" + msg.description + "'"
      + ",[emetteur] = '" + msg.emetteur + "'"

     + " ,[recepteur] = '" + msg.recepteur + "'  where ([id_message]=" + msg.id_message + ") ";
            listescmd.Add(cmd);
            cmd = new SqlCommand();



            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool delete_Message(ref string erreur, int id)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string sql = "DELETE FROM  [Message]  WHERE [id_message]=" + id + "";
            bool ins = ConnexionDB.ExecuteSql(ref erreur, sql, ConnexionDB.MaConn);
            return ins;
        }
        public List<Message> ListeMessages()
        {
            List<Message> liste = new List<Message>();
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string  query = " SELECT * FROM            Message ";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Message lignes = new Message();


                lignes.id_message = retournInt(ds.Tables[0].Rows[i]["id_message"].ToString());

                lignes.id_user = retournInt(ds.Tables[0].Rows[i]["id_user"].ToString());
                lignes.nomMembre = ds.Tables[0].Rows[i]["nomMembre"].ToString();
                lignes.sujet = ds.Tables[0].Rows[i]["sujet"].ToString();
                lignes.description = ds.Tables[0].Rows[i]["description"].ToString();
                lignes.emetteur = ds.Tables[0].Rows[i]["emetteur"].ToString();
                lignes.recepteur = ds.Tables[0].Rows[i]["recepteur"].ToString();

            }
            return liste;

        }




        #endregion type
        #region gestion Postuler
        public DataSet ListPostuler()
        {
            string Connect = chaine_connexion_Traitement();

            ConnexionDB.MaConn = new SqlConnection(Connect);

            string query = " SELECT       * FROM            Postuler";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public string GetPasswordPostuler(int code)
        {

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT   password  FROM Postuler where id_post = " + code + "";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string budgee = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                budgee = (ds.Tables[0].Rows[i]["budgee"].ToString());
            }
            return budgee;


        }
        public Postuler GetPostuler(int code)
        {
            Postuler lignes = new Postuler();

            string chaine = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(chaine);
            string query = "SELECT *FROM Postuler where id_post = " + code + "";
            DataAdapter DataTable = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            DataTable.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lignes.id_post = retournInt(ds.Tables[0].Rows[i]["id_post"].ToString());

                lignes.id_personne = retournInt(ds.Tables[0].Rows[i]["id_personne"].ToString());
                lignes.description = ds.Tables[0].Rows[i]["description"].ToString();
                lignes.datePostulation = ds.Tables[0].Rows[i]["datePostulation"].ToString();
                lignes.budgee = ds.Tables[0].Rows[i]["budgee"].ToString();
                lignes.duree = ds.Tables[0].Rows[i]["duree"].ToString();

            }
            return lignes;

        }
        public bool ajout_Postuler(ref string erreur, Postuler post)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();
            if (post.id_post == 0)
            {
                cmd.CommandText = "INSERT INTO [dbo].[Postuler]([id_post],[id_personne],[description],[datePostulation],[budgee],[duree])VALUES('" + post.id_post + "','" + post.id_personne + "','" + post.description + "','" + post.datePostulation + "','" + post.budgee + "','" + post.duree + "')";
                listescmd.Add(cmd);
            }
            else
            {
                cmd.CommandText = "UPDATE       Postuler   SET [budgee] ='" + post.budgee + "'"
     + " ,[id_personne] = '" + post.id_personne + "'"
      + ",[description] = '" + post.description + "'"
      + ",[datePostulation] = '" + post.datePostulation + "'"
     + " ,[duree] = '" + post.duree + "'  where ([id_post]=" + post.id_post + ") ";
                listescmd.Add(cmd);
                cmd = new SqlCommand();

            }
            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool modifier_Compte(ref string erreur, Postuler post)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            SqlCommand cmd;
            cmd = new SqlCommand();
            List<SqlCommand> listescmd = new List<SqlCommand>();


            cmd.CommandText = "UPDATE       Postuler   SET [budgee] ='" + post.budgee + "'"
     + " ,[id_personne] = '" + post.id_personne + "'"
      + ",[description] = '" + post.description + "'"
      + ",[datePostulation] = '" + post.datePostulation + "'"
     + " ,[duree] = '" + post.duree + "'  where ([id_post]=" + post.id_post + ") ";
            listescmd.Add(cmd);
            cmd = new SqlCommand();




            bool ins = ConnexionDB.ExecuteSqlWithTransaction(ref erreur, Connect, listescmd);
            return ins;
        }
        public bool delete_Postuler(ref string erreur, int id)
        {
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string sql = "DELETE FROM  [Postuler]  WHERE [id_post]=" + id + "";
            bool ins = ConnexionDB.ExecuteSql(ref erreur, sql, ConnexionDB.MaConn);
            return ins;
        }
        public List<Postuler> ListePostulers()
        {
            List<Postuler> liste = new List<Postuler>();
            string Connect = chaine_connexion_Traitement();
            ConnexionDB.MaConn = new SqlConnection(Connect);
            string query =
                  query = " SELECT * FROM            Postuler ";
            DataAdapter da = ConnexionDB.GetResultSql(query, ConnexionDB.MaConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Postuler lignes = new Postuler();


                lignes.id_post = retournInt(ds.Tables[0].Rows[i]["id_post"].ToString());

                lignes.id_personne = retournInt(ds.Tables[0].Rows[i]["id_personne"].ToString());
                lignes.description = ds.Tables[0].Rows[i]["description"].ToString();
                lignes.datePostulation = ds.Tables[0].Rows[i]["datePostulation"].ToString();
                lignes.budgee = ds.Tables[0].Rows[i]["budgee"].ToString();
                lignes.duree = ds.Tables[0].Rows[i]["duree"].ToString();
                liste.Add(lignes);
            }
            return liste;

        }

        #endregion
   }



}
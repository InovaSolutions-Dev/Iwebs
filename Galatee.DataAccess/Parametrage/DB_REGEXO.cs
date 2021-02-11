﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Galatee.Structure;
using Galatee.Entity.Model;

namespace Galatee.DataAccess
{
    /// <summary>
    /// DB_BANQUES2
    /// </summary>
    public class DB_REGEXO : Galatee.DataAccess.Parametrage.DbBase
    {
        /*
        /// <summary>
        /// DB_BANQUES2
        /// </summary>
        public DB_REGEXO()
        {
            ConnectionString = Session.GetSqlConnexionString();
        }
        /// <summary>
        /// DB_BANQUES2
        /// </summary>
        /// <param name="ConnStr"></param>
        public DB_REGEXO(string ConnStr)
        {
            ConnectionString = ConnStr;
        }
        /// <summary>
        /// ConnectionString
        /// </summary>
        private string ConnectionString;
        private SqlConnection cn = null;

        private bool _Transaction;
        /// <summary>
        /// Transaction
        /// </summary>
        public bool Transaction
        {
            get { return _Transaction; }
            set { _Transaction = value; }

        }

        private SqlCommand cmd = null;


        public List<CsRegExo> SelectAllRegExo()
        {
            cn = new SqlConnection(ConnectionString);
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                cmd = new SqlCommand
                {
                    Connection = cn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = EnumProcedureStockee.SelectREGEXO
                };
                IDataReader reader = cmd.ExecuteReader();
                var rows = new List<CsRegExo>();
                Fill(reader, rows, int.MinValue, int.MaxValue);
                reader.Close();
                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception(EnumProcedureStockee.SelectREGEXO + ":" + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close(); // Fermeture de la connection 
                cmd.Dispose();
            }
        }

        public bool Delete(CsRegExo pRegExo)
        {
            try
            {
                cn = new SqlConnection(ConnectionString);
                cmd = new SqlCommand
                {
                    Connection = cn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = EnumProcedureStockee.DeleteREGEXO
                };
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CENTRE", pRegExo.CENTRE);
                cmd.Parameters.AddWithValue("@PRODUIT", pRegExo.PRODUIT);
                cmd.Parameters.AddWithValue("@REGCLI", pRegExo.REGCLI);
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                StartTransaction(cn);
                int rowsAffected = cmd.ExecuteNonQuery();
                CommitTransaction(cmd.Transaction);
                return Convert.ToBoolean(rowsAffected);
            }
            catch (Exception ex)
            {
                RollBackTransaction(cmd.Transaction);
                throw new Exception(EnumProcedureStockee.DeleteREGEXO + ":" + ex.Message);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cmd.Dispose();
            }
        }

        public bool Delete(List<CsRegExo> pRegExoCollection)
        {
            int number = 0;
            foreach (CsRegExo entity in pRegExoCollection)
            {
                if (Delete(entity))
                {
                    number++;
                }
            }
            return number != 0;
        }

        private bool Update(CsRegExo pRegExo)
        {
            cn = new SqlConnection(ConnectionString);
            cmd = new SqlCommand
            {
                Connection = cn,
                CommandType = CommandType.StoredProcedure,
                CommandText = EnumProcedureStockee.UpdateREGEXO
            };
            cmd.Parameters.Clear();

            try
            {

                cmd.Parameters.AddWithValue("@CENTRE", pRegExo.CENTRE);
                cmd.Parameters.AddWithValue("@OriginalCENTRE", pRegExo.OriginalCENTRE);
                cmd.Parameters.AddWithValue("@PRODUIT", pRegExo.PRODUIT);
                cmd.Parameters.AddWithValue("@OriginalPRODUIT", pRegExo.OriginalPRODUIT);
                cmd.Parameters.AddWithValue("@REGCLI", pRegExo.REGCLI);
                cmd.Parameters.AddWithValue("@OriginalREGCLI", pRegExo.OriginalREGCLI);
                cmd.Parameters.AddWithValue("@EXFAV", pRegExo.EXFAV);
                cmd.Parameters.AddWithValue("@EXFDOS", pRegExo.EXFDOS);
                cmd.Parameters.AddWithValue("@EXFPOL", pRegExo.EXFPOL);
                cmd.Parameters.AddWithValue("@TRAITFAC", pRegExo.TRAITFAC);
                cmd.Parameters.AddWithValue("@TRANS", pRegExo.TRANS);
                cmd.Parameters.AddWithValue("@REFERENCEPUPITRE", pRegExo.REFERENCEPUPITRE);
                cmd.Parameters.AddWithValue("@DATECREATION", pRegExo.DATECREATION);
                cmd.Parameters.AddWithValue("@DATEMODIFICATION", pRegExo.DATEMODIFICATION);
                cmd.Parameters.AddWithValue("@USERCREATION", pRegExo.USERCREATION);
                cmd.Parameters.AddWithValue("@USERMODIFICATION", pRegExo.USERMODIFICATION);

                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                StartTransaction(cn);
                SetDBNullParametre(cmd.Parameters);
                int rowsAffected = cmd.ExecuteNonQuery();
                CommitTransaction(cmd.Transaction);
                return Convert.ToBoolean(rowsAffected);
            }
            catch (Exception ex)
            {
                RollBackTransaction(cmd.Transaction);
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close(); // Fermeture de la connection 
                cmd.Dispose();
            }
        }

        public bool Update(List<CsRegExo> pRegExoCollection)
        {
            int number = 0;
            foreach (CsRegExo entity in pRegExoCollection)
            {
                if (Update(entity))
                {
                    number++;
                }
            }
            return number != 0;
        }

        private bool Insert(CsRegExo pRegExo)
        {
            cn = new SqlConnection(ConnectionString);
            cmd = new SqlCommand
            {
                Connection = cn,
                CommandType = CommandType.StoredProcedure,
                CommandText = EnumProcedureStockee.InsertREGEXO
            };
            cmd.Parameters.Clear();

            try
            {
                cmd.Parameters.AddWithValue("@CENTRE", pRegExo.CENTRE);
                cmd.Parameters.AddWithValue("@PRODUIT", pRegExo.PRODUIT);
                cmd.Parameters.AddWithValue("@REGCLI", pRegExo.REGCLI);
                cmd.Parameters.AddWithValue("@EXFAV", pRegExo.EXFAV);
                cmd.Parameters.AddWithValue("@EXFDOS", pRegExo.EXFDOS);
                cmd.Parameters.AddWithValue("@EXFPOL", pRegExo.EXFPOL);
                cmd.Parameters.AddWithValue("@TRAITFAC", pRegExo.TRAITFAC);
                cmd.Parameters.AddWithValue("@TRANS", pRegExo.TRANS);
                cmd.Parameters.AddWithValue("@REFERENCEPUPITRE", pRegExo.REFERENCEPUPITRE);
                cmd.Parameters.AddWithValue("@DATECREATION", pRegExo.DATECREATION);
                cmd.Parameters.AddWithValue("@DATEMODIFICATION", pRegExo.DATEMODIFICATION);
                cmd.Parameters.AddWithValue("@USERCREATION", pRegExo.USERCREATION);
                cmd.Parameters.AddWithValue("@USERMODIFICATION", pRegExo.USERMODIFICATION);

                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                StartTransaction(cn);

                SetDBNullParametre(cmd.Parameters);
                int rowsAffected = cmd.ExecuteNonQuery();
                CommitTransaction(cmd.Transaction);
                return Convert.ToBoolean(rowsAffected);
            }
            catch (Exception ex)
            {
                RollBackTransaction(cmd.Transaction);
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close(); // Fermeture de la connection 
                cmd.Dispose();
            }
        }

        public bool Insert(List<CsRegExo> pRegExoCollection)
        {
            int number = 0;
            foreach (CsRegExo entity in pRegExoCollection)
            {
                if (Insert(entity))
                {
                    number++;
                }
            }
            return number != 0;
        }

        public static List<CsRegExo> Fill(IDataReader reader, List<CsRegExo> rows, int start, int pageLength)
        {
            // advance to the starting row
            for (int i = 0; i < start; i++)
            {
                if (!reader.Read())
                    return rows; // not enough rows, just return
            }

            for (int i = 0; i < pageLength; i++)
            {
                if (!reader.Read())
                    break; // we are done

                var c = new CsRegExo();
                c.REGCLI = (Convert.IsDBNull(reader["REGCLI"])) ? string.Empty : (System.String)reader["REGCLI"];
                c.OriginalREGCLI = (Convert.IsDBNull(reader["REGCLI"])) ? string.Empty : (System.String)reader["REGCLI"];
                c.PRODUIT = (Convert.IsDBNull(reader["PRODUIT"])) ? string.Empty : (System.String)reader["PRODUIT"];
                c.OriginalPRODUIT = (Convert.IsDBNull(reader["PRODUIT"])) ? string.Empty : (System.String)reader["PRODUIT"];
                c.CENTRE = (Convert.IsDBNull(reader["CENTRE"])) ? string.Empty : (System.String)reader["CENTRE"];
                c.OriginalCENTRE = (Convert.IsDBNull(reader["CENTRE"])) ? string.Empty : (System.String)reader["CENTRE"];
                c.EXFAV = (Convert.IsDBNull(reader["EXFAV"])) ? string.Empty : (System.String)reader["EXFAV"];
                c.EXFDOS = (Convert.IsDBNull(reader["EXFDOS"])) ? string.Empty : (System.String)reader["EXFDOS"];
                c.EXFPOL = (Convert.IsDBNull(reader["EXFPOL"])) ? string.Empty : (System.String)reader["EXFPOL"];
                c.TRAITFAC = (Convert.IsDBNull(reader["TRAITFAC"])) ? string.Empty : (System.String)reader["TRAITFAC"];
                c.TRANS = (Convert.IsDBNull(reader["TRANS"])) ? string.Empty : (System.String)reader["TRANS"];
                c.REFERENCEPUPITRE = (Convert.IsDBNull(reader["REFERENCEPUPITRE"])) ? null : (short?)reader["REFERENCEPUPITRE"];
                c.DATECREATION = (Convert.IsDBNull(reader["DATECREATION"])) ? DateTime.MinValue : (System.DateTime)reader["DATECREATION"];
                if (Convert.IsDBNull(reader["DATEMODIFICATION"]))
                    c.DATECREATION = null;
                else
                    c.DATEMODIFICATION = (System.DateTime)reader["DATEMODIFICATION"];
                c.USERCREATION = (Convert.IsDBNull(reader["USERCREATION"])) ? string.Empty : (System.String)reader["USERCREATION"];
                c.USERMODIFICATION = (Convert.IsDBNull(reader["USERMODIFICATION"])) ? null : (System.String)reader["USERMODIFICATION"];
                c.LIBELLECENTRE = (Convert.IsDBNull(reader["LIBELLECENTRE"])) ? null : (System.String)reader["LIBELLECENTRE"];
                c.LIBELLEPRODUIT = (Convert.IsDBNull(reader["LIBELLEPRODUIT"])) ? null : (System.String)reader["LIBELLEPRODUIT"];
                c.LIBELLEREGCLI = (Convert.IsDBNull(reader["LIBELLEREGCLI"])) ? null : (System.String)reader["LIBELLEREGCLI"];
                rows.Add(c);
            }
            return rows;
        }
		
        /// <summary>
        /// StartTransaction
        /// </summary>
        /// <param name="_conn"></param>
        private void StartTransaction(SqlConnection _conn)
        {
            if ((_Transaction) && (_conn != null))
            {
                cmd.Transaction = this.BeginTransaction(_conn);
            }
        }
        /// <summary>
        /// CommitTransaction
        /// </summary>
        /// <param name="_pSqlTransaction"></param>
        private void CommitTransaction(SqlTransaction _pSqlTransaction)
        {
            if ((_Transaction) && (_pSqlTransaction != null))
            {
                this.Commit(_pSqlTransaction);
            }
        }
        /// <summary>
        /// RollBackTransaction
        /// </summary>
        /// <param name="_pSqlTransaction"></param>
        private void RollBackTransaction(SqlTransaction _pSqlTransaction)
        {
            if ((_Transaction) && (_pSqlTransaction != null))
            {
                this.RollBack(_pSqlTransaction);
            }

        }
        */

        public List<CsRegExo> SelectAllRegExo()
        {
            try
            {
                return Entities.GetEntityListFromQuery<CsRegExo>(ParamProcedure.PARAM_REGEXO_RETOURNE());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(CsRegExo pRegExo)
        {
            try
            {
                return Entities.DeleteEntity<Galatee.Entity.Model.REGEXO>(Entities.ConvertObject<Galatee.Entity.Model.REGEXO, CsRegExo>(pRegExo));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<CsRegExo> pRegExoCollection)
        {
            try
            {
                return Entities.DeleteEntity<Galatee.Entity.Model.REGEXO>(Entities.ConvertObject<Galatee.Entity.Model.REGEXO, CsRegExo>(pRegExoCollection));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Update(CsRegExo pRegExo)
        {
            try
            {
                return Entities.UpdateEntity<Galatee.Entity.Model.REGEXO>(Entities.ConvertObject<Galatee.Entity.Model.REGEXO, CsRegExo>(pRegExo));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(List<CsRegExo> pRegExoCollection)
        {
            try
            {
                return Entities.UpdateEntity<Galatee.Entity.Model.REGEXO>(Entities.ConvertObject<Galatee.Entity.Model.REGEXO, CsRegExo>(pRegExoCollection));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Insert(CsRegExo pRegExo)
        {
            try
            {
                return Entities.InsertEntity<Galatee.Entity.Model.REGEXO>(Entities.ConvertObject<Galatee.Entity.Model.REGEXO, CsRegExo>(pRegExo));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(List<CsRegExo> pRegExoCollection)
        {
            try
            {
                return Entities.InsertEntity<Galatee.Entity.Model.REGEXO>(Entities.ConvertObject<Galatee.Entity.Model.REGEXO, CsRegExo>(pRegExoCollection));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Galatee.Silverlight.Library;
using Galatee.Silverlight.ServiceParametrage;
using Galatee.Silverlight.Resources.Parametrage;
namespace Galatee.Silverlight.Parametrage
{
    public partial class UcSecteur : ChildWindow
    {
        List<CsSecteur> listForInsertOrUpdate = null;
        ObservableCollection<CsSecteur> donnesDatagrid = new ObservableCollection<CsSecteur>();
        private CsSecteur ObjetSelectionnee = null;
        private Object ModeExecution = null;
        private DataGrid dataGrid = null;

        public UcSecteur()
        {
            try
            {
                InitializeComponent();
                Translate();
            }
            catch (Exception ex)
            {
                Message.ShowError(ex.Message, "Secteur");
            }
        }
        public UcSecteur(CsSecteur pObject, SessionObject.ExecMode pExecMode, DataGrid pGrid)
        {
            try
            {
                InitializeComponent();
                Translate();
                var Secteur = new CsSecteur();
                if (pObject != null)
                    ObjetSelectionnee = Utility.ParseObject(Secteur, pObject as CsSecteur);
                ModeExecution = pExecMode;
                dataGrid = pGrid;
                //RemplirListeDesCentreExistant();
                RemplirQuartier();
                if (dataGrid != null) donnesDatagrid = dataGrid.ItemsSource as ObservableCollection<CsSecteur>;
                if ((SessionObject.ExecMode)ModeExecution == SessionObject.ExecMode.Modification || (SessionObject.ExecMode)ModeExecution == SessionObject.ExecMode.Consultation)
                {
                    if (ObjetSelectionnee != null)
                    {
                        Txt_Code.Text = ObjetSelectionnee.CODE;
                        Txt_Libelle.Text = ObjetSelectionnee.LIBELLE ;
                        //CboQuartier.SelectedItem = SessionObject.LstQuartier.FirstOrDefault(a => a.PK_ID == ObjetSelectionnee.FK_IDQUARTIER);
                        btnOk.IsEnabled = false;
                        //Txt_Code.IsReadOnly = true;
                    }
                }
                if ((SessionObject.ExecMode)ModeExecution == SessionObject.ExecMode.Consultation)
                {
                    AllInOne.ActivateControlsFromXaml(LayoutRoot,false);
                }
                VerifierSaisie();
            }
            catch (Exception ex)
            {
                Message.ShowError(ex.Message, "Secteur");
            }
        }

        private void UpdateParentList(CsSecteur pSecteur)
        {
            try
            {
                if ((SessionObject.ExecMode)ModeExecution == SessionObject.ExecMode.Creation)
                {
                    donnesDatagrid.Add(pSecteur);
                    donnesDatagrid.OrderBy(p => p.PK_ID);
                }
                if ((SessionObject.ExecMode)ModeExecution == SessionObject.ExecMode.Modification)
                {
                    var Secteurs = donnesDatagrid.First(p => p.PK_ID == pSecteur.PK_ID);
                    donnesDatagrid.Remove(Secteurs);
                    donnesDatagrid.Add(pSecteur);
                    donnesDatagrid.OrderBy(p => p.PK_ID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Translate()
        {
            try
            {
                //Title = "Secteur";
                //btnOk.Content = Languages.OK;
                //Btn_Reinitialiser.Content = Languages.Annuler;
                //GboCodeDepart.Header = Languages.InformationsCodePoste;
                //lab_Code.Content = Languages.Code;
                //lab_Libelle.Content = Languages.Libelle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<CsSecteur> GetInformationsFromScreen()
        {
            var listObjetForInsertOrUpdate = new List<CsSecteur>();
            try
            {
                if ((SessionObject.ExecMode)ModeExecution == SessionObject.ExecMode.Creation)
                {
                    var secteur = new CsSecteur
                    {
                        CODE  = Txt_Code.Text,
                        FK_IDQUARTIER = ((CsQuartier)CboQuartier.SelectedItem).PK_ID,
                        CODEQUARTIER = ((CsQuartier)CboQuartier.SelectedItem).CODE,
                        LIBELLE  = Txt_Libelle.Text,
                        DATECREATION = DateTime.Now,
                        USERCREATION = UserConnecte.matricule
                    };
                    if (!string.IsNullOrEmpty(Txt_Code.Text) && donnesDatagrid.FirstOrDefault(p => p.CODE == secteur.CODE && p.FK_IDQUARTIER == secteur.FK_IDQUARTIER) != null)
                    {
                        throw new Exception(Languages.CetElementExisteDeja);
                    }
                    listObjetForInsertOrUpdate.Add(secteur);
                }
                if ((SessionObject.ExecMode)ModeExecution == SessionObject.ExecMode.Modification)
                {
                    ObjetSelectionnee.CODE = Txt_Code.Text;
                    ObjetSelectionnee.LIBELLE  = Txt_Libelle.Text;
                    ObjetSelectionnee.FK_IDQUARTIER = ((CsQuartier)CboQuartier.SelectedItem).PK_ID;
                    ObjetSelectionnee.CODEQUARTIER = ((CsQuartier)CboQuartier.SelectedItem).CODE;
                    ObjetSelectionnee.DATECREATION = DateTime.Now;
                    ObjetSelectionnee.USERCREATION = UserConnecte.matricule;
                    listObjetForInsertOrUpdate.Add(ObjetSelectionnee);
                }
                return listObjetForInsertOrUpdate;
            }
            catch (Exception ex)
            {
                Message.ShowError(ex.Message, "Secteur");
                return null;
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var messageBox = new MessageBoxControl.MessageBoxChildWindow("Secteur", Languages.QuestionEnregistrerDonnees, MessageBoxControl.MessageBoxButtons.YesNo, MessageBoxControl.MessageBoxIcon.Question);
                messageBox.OnMessageBoxClosed += (_, result) =>
                {
                    if (messageBox.Result == MessageBoxResult.OK)
                    {
                        listForInsertOrUpdate = GetInformationsFromScreen();
                        var service = new ParametrageClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Parametrage"));

                        if (listForInsertOrUpdate != null && listForInsertOrUpdate.Count > 0)
                        {
                            if ((SessionObject.ExecMode)ModeExecution == SessionObject.ExecMode.Creation)
                            {
                                service.InsertSecteurCompleted += (snder, insertR) =>
                                {
                                    if (insertR.Cancelled ||
                                        insertR.Error != null)
                                    {
                                        Message.ShowError(insertR.Error.Message, "Secteur");
                                        return;
                                    }
                                    if (!insertR.Result)
                                    {
                                        Message.ShowError(Languages.ErreurInsertionDonnees, "Secteur");
                                        return;
                                    }
                                    UpdateParentList(listForInsertOrUpdate[0]);
                                    DialogResult = true;
                                };
                                service.InsertSecteurAsync(listForInsertOrUpdate);
                            }
                            if ((SessionObject.ExecMode)ModeExecution == SessionObject.ExecMode.Modification)
                            {
                                service.UpdateSecteurCompleted += (snder, UpdateR) =>
                                {
                                    if (UpdateR.Cancelled ||
                                        UpdateR.Error != null)
                                    {
                                        Message.ShowError(UpdateR.Error.Message, "Secteur");
                                        return;
                                    }
                                    if (!UpdateR.Result)
                                    {
                                        Message.ShowError(Languages.ErreurMiseAJourDonnees, "Secteur");
                                        return;
                                    }
                                    UpdateParentList(listForInsertOrUpdate[0]);
                                    DialogResult = true;
                                };
                                service.UpdateSecteurAsync(listForInsertOrUpdate);
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                };
                messageBox.Show();
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message, "Secteur");
            }
        }

        //private void RemplirListeDesCentreExistant()
        //{
        //    try
        //    {
        //        ParametrageClient client = new ParametrageClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Parametrage"));
        //        client.SelectAllCentreCompleted += (ssender, args) =>
        //        {
        //            if (args.Cancelled || args.Error != null)
        //            {
        //                string error = args.Error.Message;
        //                Message.ShowError(error, Silverlight.Resources.Devis.Languages.txtDevis);
        //                return;
        //            }
        //            if (args.Result == null)
        //            {
        //                Message.ShowError(Languages.msgErreurChargementDonnees, Languages.Parametrage);
        //                return;
        //            }
        //            //else
        //            //{
        //            //    this.CboCentre.ItemsSource = args.Result;
        //            //    this.CboCentre.DisplayMemberPath = "PK_ID";
        //            //    this.CboCentre.SelectedValuePath = "LIBELLE";

        //            //    if (ObjetSelectionnee != null)
        //            //    {
        //            //        foreach (CsCentre centre in CboCentre.ItemsSource)
        //            //        {
        //            //            if (centre.PK_ID == ObjetSelectionnee.FK_IDCENTRE)
        //            //            {
        //            //                CboCentre.SelectedItem = centre;
        //            //                break;
        //            //            }
        //            //        }
        //            //    }
        //            //}
        //        };
        //        client.SelectAllCentreAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        List<CsQuartier> ListQuartier = new List<CsQuartier>();
        private void RemplirQuartier()
        {
            try
            {
                ParametrageClient client = new ParametrageClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Parametrage"));
                client.SelectAllQuartierCompleted += (ssender, args) =>
                {
                    if (args.Cancelled || args.Error != null)
                    {
                        string error = args.Error.Message;
                        Message.Show(error, "Secteur");
                        return;
                    }
                    if (args.Result == null)
                    {
                        Message.ShowError(Languages.msgErreurChargementDonnees, Languages.Parametrage);
                        return;
                    }
                    else
                    {
                        ListQuartier.AddRange(args.Result);
                        CboQuartier.ItemsSource = args.Result;
                        CboQuartier.SelectedValuePath = "PK_ID";
                        CboQuartier.DisplayMemberPath = "LIBELLE";

                        if (ObjetSelectionnee != null)
                        {
                            foreach (CsQuartier st in CboQuartier.ItemsSource)
                            {
                                if (st.PK_ID == ObjetSelectionnee.FK_IDQUARTIER)
                                {
                                    CboQuartier.SelectedItem = st;
                                    break;
                                }
                            }
                        }
                    }
                };
                client.SelectAllQuartierAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        private void VerifierSaisie()
        {
            try
            {
                if (!string.IsNullOrEmpty(Txt_Code.Text) && !string.IsNullOrEmpty(Txt_Libelle.Text) && (SessionObject.ExecMode)ModeExecution != SessionObject.ExecMode.Consultation
                    && CboQuartier.SelectedItem != null && CboQuartier.SelectedItem != null)
                    btnOk.IsEnabled = true;
                else
                {
                    btnOk.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Btn_Annuler_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Reinitialiser();
                DialogResult = false;
            }
            catch (Exception ex)
            {
                Message.ShowError(ex.Message, "Secteur");
            }
        }

        private void Reinitialiser()
        {
            try
            {
                Txt_Code.Text = string.Empty;
                Txt_Libelle.Text = string.Empty;
                CboQuartier.SelectedItem = null;
                btnOk.IsEnabled = false;
                Txt_Code.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void On_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                VerifierSaisie();
            }
            catch (Exception ex)
            {
                Message.ShowError(ex.Message, "Secteur");
            }
        }

        private void OnComboSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                VerifierSaisie();
            }
            catch (Exception ex)
            {
                Message.ShowError(ex.Message, "Secteur");
            }
        }
    }
}



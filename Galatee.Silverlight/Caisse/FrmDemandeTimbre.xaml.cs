using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Galatee.Silverlight.ServiceCaisse;
using Galatee.Silverlight.ServiceAccueil;
using System.Collections.ObjectModel;
using System.IO;
using Galatee.Silverlight.Shared;
namespace Galatee.Silverlight.Caisse
{
    public partial class FrmDemandeTimbre : ChildWindow
    {

        public FrmDemandeTimbre()
        {
            InitializeComponent();
            RetourneTypeTimbre();
            ChargerTypeDocument();
        }

 
        List<ServiceAccueil.CsElementAchatTimbre> lstObjetDevis = new List<ServiceAccueil.CsElementAchatTimbre>();
        public ObservableCollection<CsTypeDOCUMENTSCANNE> LstTypeDocument = new ObservableCollection<CsTypeDOCUMENTSCANNE>();
        CsDemande laDetailDemande = null;

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            btn_Transmetre.IsEnabled = false;
            ValiderInitialisation(laDetailDemande, true );
        }
        private void ValiderInitialisation(CsDemande demandedevis, bool IsTransmetre)
        {

            try
            {
                // Get Devis informations from screen
                if (demandedevis != null)
                    demandedevis = GetDemandeDevisFromScreen(demandedevis, false);
                else
                    demandedevis = GetDemandeDevisFromScreen(null, false);
                // Get DemandeDevis informations from screen
                if (demandedevis != null)
                {
                    if (IsTransmetre)
                        demandedevis.LaDemande.ETAPEDEMANDE = null;
                    demandedevis.LaDemande.MATRICULE = UserConnecte.matricule;
                    demandedevis.LaDemande.CLIENT = "00000000000";
                    demandedevis.LaDemande.ORDRE = "01";
                    if (SessionObject.LePosteCourant.CODECENTRE == SessionObject.Enumere.Generale)
                    {
                      
                        demandedevis.LaDemande.CENTRE = "011";
                        demandedevis.LaDemande.FK_IDCENTRE = 63;
                    }
                    else
                    {
                        demandedevis.LaDemande.CENTRE = SessionObject.LePosteCourant.CODECENTRE;
                        demandedevis.LaDemande.FK_IDCENTRE = SessionObject.LePosteCourant.FK_IDCENTRE.Value ;
                    }

                    #region Doc Scanne
                    if (demandedevis.ObjetScanne == null) demandedevis.ObjetScanne = new List<ObjDOCUMENTSCANNE>();
                    demandedevis.ObjetScanne.AddRange(LstPiece);
                    #endregion


                    Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient client = new Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
                    client.CreeDemandeCompleted += (ss, b) =>
                    {
                        DialogResult = true;
                        if (b.Cancelled || b.Error != null)
                        {
                            string error = b.Error.Message;
                            Message.ShowError(error, Silverlight.Resources.Devis.Languages.txtDevis);
                            return;
                        }
                        if (b.Result != null)
                        {
                            Message.ShowInformation("La demande a été créée avec succès. Numéro de votre demande : " + b.Result.NUMDEM,
                                 Silverlight.Resources.Devis.Languages.txtDevis);
                        }
                        else
                            Message.ShowError("Une erreur s'est produite à la création de la demande ", "CreeDemande");
                    };
                    client.CreeDemandeAsync(demandedevis, true);
                }
            }
            catch (Exception ex)
            {
                Message.ShowError("Une erreur est survenu suite à la validation", "Validation demande");
            }
        }
         
        private CsDemande GetDemandeDevisFromScreen(CsDemande pDemandeDevis, bool isTransmettre)
        {
            if (pDemandeDevis == null)
            {
                pDemandeDevis = new CsDemande();
                pDemandeDevis.LaDemande = new CsDemandeBase();
                pDemandeDevis.LaDemande.DATECREATION = DateTime.Now;
                pDemandeDevis.LaDemande.USERCREATION = UserConnecte.matricule;
                pDemandeDevis.LaDemande.FK_IDADMUTILISATEUR = UserConnecte.PK_ID;
                pDemandeDevis.LaDemande.TYPEDEMANDE = SessionObject.LstTypeDemande.FirstOrDefault(t => t.CODE == SessionObject.Enumere.AchatTimbre ).CODE;
                pDemandeDevis.LaDemande.FK_IDTYPEDEMANDE = SessionObject.LstTypeDemande.FirstOrDefault(t => t.CODE == SessionObject.Enumere.AchatTimbre).PK_ID;
            }
            if (pDemandeDevis.LaDemande == null) pDemandeDevis.LaDemande = new CsDemandeBase();
            pDemandeDevis.LaDemande.DATEMODIFICATION = DateTime.Now;
            pDemandeDevis.LstEltTimbre = lstObjetDevis;

            return pDemandeDevis;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }


        private void ChargerTypeDocument()
        {
            try
            {
                Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient service = new Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient(Utility.Protocole(), Utility.EndPoint("Accueil"));
                service.ChargerTypeDocumentCompleted += (s, args) =>
                {
                    if ((args != null && args.Cancelled) || (args.Error != null))
                        return;

                    foreach (var item in args.Result)
                    {
                        LstTypeDocument.Add(item);
                    }
                    cbo_typedoc.ItemsSource = LstTypeDocument;
                    cbo_typedoc.DisplayMemberPath = "LIBELLE";
                    cbo_typedoc.SelectedValuePath = "PK_ID";

                    //this.Resources.Add("FuelList", LstTypeDocument);

                };
                service.ChargerTypeDocumentAsync();
                service.CloseAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }


        private void RetourneTypeTimbre()
        {
             CaisseServiceClient service = new  CaisseServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Caisse"));
             service.RetouneTypeTimbreCompleted  += (s, args) =>
            {
                if (args != null && args.Cancelled)
                    return;
                cbo_TypeTimbre.ItemsSource = args.Result;
                cbo_TypeTimbre.DisplayMemberPath = "LIBELLE";

            };
             service.RetouneTypeTimbreAsync();
            service.CloseAsync();
        }

        private void Valider_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.cbo_TypeTimbre.SelectedItem != null && !string.IsNullOrEmpty(this.Txt_Nombre.Text))
            {
                ServiceAccueil.CsElementAchatTimbre leElt = new ServiceAccueil.CsElementAchatTimbre()
                {
                    CODE = ((CsTypeTimbre)cbo_TypeTimbre.SelectedItem).CODE,
                    DESIGNATION = ((CsTypeTimbre)cbo_TypeTimbre.SelectedItem).LIBELLE,
                    PRIXUNITAIRE = ((CsTypeTimbre)cbo_TypeTimbre.SelectedItem).MONTANT,
                    QUANTITE = int.Parse(Txt_Nombre.Text),
                    MONTANT = ((CsTypeTimbre)cbo_TypeTimbre.SelectedItem).MONTANT * int.Parse(Txt_Nombre.Text),
                    USERCREATION = UserConnecte.matricule ,
                    DATECREATION = System.DateTime.Today.Date  ,
                    FK_IDTIMBRE = ((CsTypeTimbre)cbo_TypeTimbre.SelectedItem).PK_ID 
                };
                ServiceAccueil.CsElementAchatTimbre leEltRech = lstObjetDevis.FirstOrDefault(t => t.CODE == leElt.CODE);
                if (leEltRech != null)
                {
                    lstObjetDevis.Remove(leEltRech);
                    lstObjetDevis.Add(leElt);
                }
                else
                    lstObjetDevis.Add(leElt);

                this.dtg_EtatCaisse.ItemsSource = null;
                this.dtg_EtatCaisse.ItemsSource = lstObjetDevis;
            }

        }

        private void cbo_TypeTimbre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbo_TypeTimbre.SelectedItem != null)
                this.Txt_Montant.Text = ((CsTypeTimbre)cbo_TypeTimbre.SelectedItem).MONTANT.Value.ToString(SessionObject.FormatMontant); 
        }








        /*18/02/2019 */
        string CheminInitialDeCopyDeFichier = string.Empty;
        string NomDeCopyDeFichier = string.Empty;
        /**/

        private UcImageScanne formScanne = null;
        public ObservableCollection<ObjDOCUMENTSCANNE> LstPiece = new ObservableCollection<ObjDOCUMENTSCANNE>();
        private byte[] image;


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (cbo_typedoc.SelectedItem != null)
            {
                // Create an instance of the open file dialog box.
                var openDialog = new OpenFileDialog();
                // Set filter options and filter index.
                openDialog.Filter =
                    "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                openDialog.FilterIndex = 1;
                openDialog.Multiselect = false;
                // Call the ShowDialog method to show the dialog box.
                bool? userClickedOk = openDialog.ShowDialog();
                // Process input if the user clicked OK.
                if (userClickedOk == true)
                {
                    if (openDialog.Files != null && openDialog.Files.Count() > 0 && openDialog.File != null)
                    {

                        /*18/02/2019 */
                        CheminInitialDeCopyDeFichier = SessionObject.CheminDocumentScanne;
                        NomDeCopyDeFichier = openDialog.Files.First().Name;


                        FileStream stream = openDialog.File.OpenRead();
                        var memoryStream = new MemoryStream();
                        stream.CopyTo(memoryStream);
                        image = memoryStream.GetBuffer();
                        formScanne = new UcImageScanne(memoryStream, SessionObject.ExecMode.Creation);
                        formScanne.Closed += new EventHandler(GetInformationFromChildWindowImagePreuve);
                        formScanne.Show();
                    }
                }
            }
        }
        private void GetInformationFromChildWindowImagePreuve(object sender, EventArgs e)
        {
            this.LstPiece.Add(new ObjDOCUMENTSCANNE
            {
                PK_ID = Guid.NewGuid(),
                NOMDOCUMENT = ((CsTypeDOCUMENTSCANNE)cbo_typedoc.SelectedItem).LIBELLE,
                FK_IDTYPEDOCUMENT = ((CsTypeDOCUMENTSCANNE)cbo_typedoc.SelectedItem).PK_ID,
                CONTENU = image,
                CODETYPEDOC = ((CsTypeDOCUMENTSCANNE)cbo_typedoc.SelectedItem).CODE,
                DATECREATION = DateTime.Now,
                DATEMODIFICATION = DateTime.Now,
                USERCREATION = UserConnecte.matricule,
                USERMODIFICATION = UserConnecte.matricule,
                ISNEW = true,
                NOMDUFICHIER = NomDeCopyDeFichier,
                CHEMININIT = CheminInitialDeCopyDeFichier + "\\" + NomDeCopyDeFichier
                //CHEMININIT = CheminInitialDeCopyDeFichier + "\\" + NomDeCopyDeFichier,
                //CHEMINCOPY = SessionObject.Enumere.CheminImpressionServeur + "\\" + laDemande.LaDemande.NUMDEM + "_" + ((CsTypeDOCUMENTSCANNE)cbo_typedoc.SelectedItem).CODE + "." + NomDeCopyDeFichier.Split('.').[1],
                //NOMDUFICHIER = "FICHIER.jpeg"
            });

            this.dgListePiece.ItemsSource = null;
            this.dgListePiece.ItemsSource = this.LstPiece.Where(o => o.ISTOREMOVE != true);
        }



        private void hyperlinkButtonPropScannee__Click(object sender, RoutedEventArgs e)
        {
            if (dgListePiece.SelectedItem != null)
            {
                ObjDOCUMENTSCANNE selectObj = (ObjDOCUMENTSCANNE)this.dgListePiece.SelectedItem;
                if (selectObj.CONTENU != null)
                {
                    MemoryStream memoryStream = new MemoryStream(selectObj.CONTENU);
                    var ucImageScanne = new UcImageScanne(memoryStream, SessionObject.ExecMode.Modification);
                    ucImageScanne.Show();
                }
                else
                {
                    Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient service = new Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
                    service.DocumentScanneContenuCompleted += (s, args) =>
                    {
                        if ((args != null && args.Cancelled) || (args.Error != null))
                            return;

                        MemoryStream memoryStream = new MemoryStream(args.Result.CONTENU);
                        var ucImageScanne = new UcImageScanne(memoryStream, SessionObject.ExecMode.Modification);
                        ucImageScanne.Show();
                    };
                    service.DocumentScanneContenuAsync(selectObj);
                    service.CloseAsync();

                }


            }
        }




        private void dgListePiece_CurrentCellChanged(object sender, EventArgs e)
        {
            dgListePiece.BeginEdit();
        }



        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var messageBox = new MessageBoxControl.MessageBoxChildWindow("Attention", "Êtes-vous sûr de vouloir supprimer la ligne?", MessageBoxControl.MessageBoxButtons.OkCancel, MessageBoxControl.MessageBoxIcon.Information);
            messageBox.OnMessageBoxClosed += (_, result) =>
            {
                if (messageBox.Result == MessageBoxResult.OK)
                {
                    ObjDOCUMENTSCANNE Fraix = (ObjDOCUMENTSCANNE)dgListePiece.SelectedItem;
                    Fraix.ISTOREMOVE = true;
                    this.dgListePiece.ItemsSource = null;
                    this.dgListePiece.ItemsSource = this.LstPiece.Where(u => u.ISTOREMOVE != true).ToList();

                }
                else
                {
                    return;
                }
            };
            messageBox.Show();
        }


        FileStream fs;
        private void OuvrirPieceJointe_Click(object sender, RoutedEventArgs e)
        {
            if (dgListePiece.SelectedItem != null)
            {
                ObjDOCUMENTSCANNE selectObj = (ObjDOCUMENTSCANNE)this.dgListePiece.SelectedItem;
                if (selectObj.CONTENU != null)
                {
                    MemoryStream memoryStream = new MemoryStream(selectObj.CONTENU);
                    var ucImageScanne = new UcImageScanne(memoryStream, SessionObject.ExecMode.Modification);
                    ucImageScanne.Show();
                }
                else
                {
                    Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient service = new Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
                    service.DocumentScanneContenuCompleted += (s, args) =>
                    {
                        if ((args != null && args.Cancelled) || (args.Error != null))
                            return;

                        MemoryStream memoryStream = new MemoryStream(args.Result.CONTENU);
                        var ucImageScanne = new UcImageScanne(memoryStream, SessionObject.ExecMode.Modification);
                        ucImageScanne.Show();
                    };
                    service.DocumentScanneContenuAsync(selectObj);
                    service.CloseAsync();
                }
            }
        }



     
    }
}


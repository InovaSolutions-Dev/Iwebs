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
using Galatee.Silverlight.Shared;
using System.IO;
namespace Galatee.Silverlight.Caisse
{
    public partial class FrmDemandeTimbreVerification : ChildWindow
    {

        public ObservableCollection<CsTypeDOCUMENTSCANNE> LstTypeDocument = new ObservableCollection<CsTypeDOCUMENTSCANNE>();
        private List<ObjDOCUMENTSCANNE> ObjetScanne = new List<ObjDOCUMENTSCANNE>();

        public FrmDemandeTimbreVerification()
        {
            InitializeComponent();
            ChargerTypeDocument();
            RetourneTypeTimbre();
        }
        public FrmDemandeTimbreVerification(int numDemande)
        {
            InitializeComponent();
            ChargerTypeDocument();
            RetourneTypeTimbre();
            ChargeDetailDEvis(numDemande);
        }
        List<ServiceAccueil.CsElementAchatTimbre> lstObjetDevis = new List<ServiceAccueil.CsElementAchatTimbre>();
        CsDemande laDetailDemande = null;
        private void ChargeDetailDEvis(int IdDemandeDevis)
        {

            Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient client = new Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
            client.ChargerDetailDemandeAsync(IdDemandeDevis, string.Empty);
            client.ChargerDetailDemandeCompleted += (ssender, args) =>
            {
                if (args.Cancelled || args.Error != null)
                {
                    LayoutRoot.Cursor = Cursors.Arrow;
                    string error = args.Error.Message;
                    Message.ShowError(error, Silverlight.Resources.Devis.Languages.txtDevis);
                    return;
                }
                if (args.Result == null)
                {
                    LayoutRoot.Cursor = Cursors.Arrow;
                    Message.ShowError(Silverlight.Resources.Devis.Languages.AucunesDonneesTrouvees, Silverlight.Resources.Devis.Languages.txtDevis);
                    return;
                }
                else
                {
                    laDetailDemande = args.Result;
                    this.dtg_EtatCaisse.ItemsSource = null;
                    this.dtg_EtatCaisse.ItemsSource = laDetailDemande.LstEltTimbre ;
                    this.Txt_NumDemande.Text = laDetailDemande.LaDemande.NUMDEM;
                    this.Txt_UserCreat.Text = laDetailDemande.LaDemande.USERCREATION ;

                    if (laDetailDemande.ObjetScanne != null && laDetailDemande.ObjetScanne.Count != 0)
                        //dgListePiece.ItemsSource = this.LstPiece;
                        dgListePiece.ItemsSource = laDetailDemande.ObjetScanne;

                }
                LayoutRoot.Cursor = Cursors.Arrow;
            };
        }
      
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        private void RetourneTypeTimbre()
        {
             CaisseServiceClient service = new  CaisseServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Caisse"));
             service.RetouneTypeTimbreCompleted  += (s, args) =>
            {
                if (args != null && args.Cancelled)
                    return;

            };
             service.RetouneTypeTimbreAsync();
            service.CloseAsync();
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
                pDemandeDevis.LaDemande.TYPEDEMANDE = SessionObject.LstTypeDemande.FirstOrDefault(t => t.CODE == "60").CODE;
                pDemandeDevis.LaDemande.FK_IDTYPEDEMANDE = SessionObject.LstTypeDemande.FirstOrDefault(t => t.CODE == "60").PK_ID;
            }
            if (pDemandeDevis.LaDemande == null) pDemandeDevis.LaDemande = new CsDemandeBase();
            pDemandeDevis.LaDemande.DATEMODIFICATION = DateTime.Now;
            return pDemandeDevis;
        }

        private void ValiderInitialisation(CsDemande demandedevis, bool IsTransmetre)
        {

            try
            {

                List<CsDemandeBase> laDema = new List<CsDemandeBase>();
                laDema.Add(demandedevis.LaDemande);
                Galatee.Silverlight.Shared.ClasseMEthodeGenerique.TransmettreDemande(laDema, true, this);


                //// Get Devis informations from screen
                //if (demandedevis != null)
                //    demandedevis = GetDemandeDevisFromScreen(demandedevis, false);
                //else
                //    demandedevis = GetDemandeDevisFromScreen(null, false);
                //// Get DemandeDevis informations from screen
                //if (demandedevis != null)
                //{
                //    if (IsTransmetre)
                //        demandedevis.LaDemande.ETAPEDEMANDE = null;
                //    demandedevis.LaDemande.MATRICULE = UserConnecte.matricule;
                //    demandedevis.LaDemande.CENTRE = SessionObject.LePosteCourant.CODECENTRE;
                //    demandedevis.LaDemande.FK_IDCENTRE = SessionObject.LePosteCourant.FK_IDCENTRE.Value;
                //    Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient client = new Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
                //    client.ValiderDemandeInitailisationCompleted += (ss, b) =>
                //    {
                //        if (b.Cancelled || b.Error != null)
                //        {
                //            string error = b.Error.Message;
                //            Message.ShowError(error, Silverlight.Resources.Devis.Languages.txtDevis);
                //            return;
                //        }
                //        if (IsTransmetre)
                //        {
                //            List<string> codes = new List<string>();
                //            codes.Add(laDetailDemande.InfoDemande.CODE);
                //            Galatee.Silverlight.Shared.ClasseMEthodeGenerique.TransmettreDemande(codes, true , this);
                //            this.DialogResult = true;
                //        }
                //        this.DialogResult = false;

                //    };
                //    client.ValiderDemandeInitailisationAsync(demandedevis);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
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


        private void Commenter_Click_1(object sender, RoutedEventArgs e)
        {
            ValiderInitialisation(laDetailDemande,false );
        }

        private void Valider_Click_2(object sender, RoutedEventArgs e)
        {
            List<string> codes = new List<string>();
            codes.Add(laDetailDemande.InfoDemande.CODE);
            Galatee.Silverlight.Shared.ClasseMEthodeGenerique.TransmettreDemande(codes, true , this);
            this.DialogResult = true;
        }








        private UcImageScanne formScanne = null;
        public List<ObjDOCUMENTSCANNE> LstPiece = new List<ObjDOCUMENTSCANNE>();
        private byte[] image, imageFraix;

        /*18/02/2019 */
        string CheminInitialDeCopyDeFichier = string.Empty;
        string NomDeCopyDeFichier = string.Empty;
        /**/

        private void dgListePiece_CurrentCellChanged(object sender, EventArgs e)
        {
            dgListePiece.BeginEdit();
        }

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










    }
}


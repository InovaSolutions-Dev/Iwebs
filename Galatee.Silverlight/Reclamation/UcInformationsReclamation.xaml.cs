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
using Galatee.Silverlight.ServiceAccueil   ;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using Galatee.Silverlight.Resources.Devis;
using Galatee.Silverlight.Library;
using Galatee.Silverlight.Shared;

namespace Galatee.Silverlight.Accueil
{
    public partial class UcInformationsReclamation : ChildWindow
    {
        public Galatee.Silverlight.SessionObject.ExecMode ExecMode {get;set;}
        CsDemandeBase laDemandeSelect = null;
        bool isPreuveSelectionnee = false;
        private UcImageScanne formScanne = null;
        public ObservableCollection<CsTypeDOCUMENTSCANNE> LstTypeDocument = new ObservableCollection<CsTypeDOCUMENTSCANNE>();
        CsDemande laDetailDemande = null;
        private List<CsCentre> _listeDesCentreExistant = null;
        private List<CsReglageCompteur> _listeDesReglageCompteurExistant = null;
        private  CsReclamationRcl ClsReclamation = new CsReclamationRcl();
        private  Galatee.Silverlight.ServiceAccueil.CsClient Client = new Galatee.Silverlight.ServiceAccueil.CsClient();

        public UcInformationsReclamation()
        {
            InitializeComponent();

            ctrl = new Galatee.Silverlight.Shared.UcFichierJoint(null, false);
            Vwb.Stretch = Stretch.None;
            Vwb.Child = ctrl;



            prgBar.Visibility = System.Windows.Visibility.Collapsed;
            this.Txt_ReferenceClient.MaxLength = SessionObject.Enumere.TailleClient ;
            prgBar.Visibility = System.Windows.Visibility.Collapsed;
            if (Tdem != SessionObject.Enumere.RemboursementAvance)
            {
                this.Txt_Ordre.Visibility = System.Windows.Visibility.Collapsed;
                this.lbl_Ordre.Visibility = System.Windows.Visibility.Collapsed;
            }
            RemplirListeDesReglageExistant();
            
            ChargerListDesSite();
            AfficherOuMasquer(tabItemCompte, false);
        }
        string Tdem = string.Empty;
        public UcInformationsReclamation(string TypeDemande,string Init)
        {
            InitializeComponent();

            ctrl = new Galatee.Silverlight.Shared.UcFichierJoint(null, false);
            Vwb.Stretch = Stretch.None;
            Vwb.Child = ctrl;

            this.Chk_EstClient.IsChecked = true;
            AfficherOuMasquerClientNonEDM(false);
            Txt_ReferenceClient.Text = string.Empty;
            this.btn_RechercheClient.IsEnabled = true;
            this.tabControle.SelectedItem = tab_demande;

            ChargerListDesSite();
            RemplirListeDesReglageExistant();

            this.Txt_ReferenceClient.MaxLength = SessionObject.Enumere.TailleClient;
            prgBar.Visibility = System.Windows.Visibility.Collapsed;

            Tdem = TypeDemande;
            this.txt_Reglage.Visibility = System.Windows.Visibility.Collapsed;
            this.label21.Visibility = System.Windows.Visibility.Collapsed;
            this.Btn_Reglage.Visibility = System.Windows.Visibility.Collapsed;

            ChargerModeReception();
            ChargerTypeReclamation();
            System.DateTime today = System.DateTime.Now;
            System.TimeSpan duration = new System.TimeSpan(8, 0, 0, 0);
            Dtp_DateRendezVous.SelectedDate = today.Add(duration);
            System.TimeSpan duration1 = new System.TimeSpan(3, 0, 0, 0);
            Dtp_DateretourSouhaite.SelectedDate = today.Add(duration1);
            Dtp_DateOuverture.SelectedDate = today;
            RemplirGroupeValidationDepannage();
            Txt_EtablirPar.Text = UserConnecte.nomUtilisateur + " (" + UserConnecte.matricule + ")";
            Txt_EtablirPar.IsReadOnly = true;
            
            AfficherOuMasquer(tabItemCompte, true);
            AfficherOuMasquer(tab_demande, true);
        }
        bool IsRejeterDemande = false;

        Galatee.Silverlight.Shared.UcFichierJoint ctrl = null;

        public UcInformationsReclamation(int idDevis)
        {
            InitializeComponent();

            ctrl = new Galatee.Silverlight.Shared.UcFichierJoint(null, false);
            Vwb.Stretch = Stretch.None;
            Vwb.Child = ctrl;

            AfficherOuMasquer(tabItemCompte, false);
            AfficherOuMasquer(tab_demande, false);
            this.btn_RechercheClient.IsEnabled = false;
            RemplirListeDesReglageExistant();
            ChargeDetailDEvis(idDevis);

        }
        
        private void ChargeDetailDEvis(int IdDemandeDevis)
        {

            AcceuilServiceClient client = new AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
            client.ChargerDetailDemandeAsync(IdDemandeDevis,string.Empty );
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
                    laDetailDemande = new CsDemande();
                    laDetailDemande = args.Result;
                    this.txtSite.Text = string.IsNullOrEmpty(laDetailDemande.LaDemande.LIBELLESITE) ? string.Empty : laDetailDemande.LaDemande.LIBELLESITE;
                    this.txtCentre.Text = string.IsNullOrEmpty(laDetailDemande.LaDemande.LIBELLECENTRE) ? string.Empty : laDetailDemande.LaDemande.LIBELLECENTRE;
                    this.Txt_ReferenceClient.Text = string.IsNullOrEmpty(laDetailDemande.LaDemande.CLIENT) ? string.Empty : laDetailDemande.LaDemande.CLIENT;
                    this.txt_Produit.Text = string.IsNullOrEmpty(laDetailDemande.LaDemande.LIBELLEPRODUIT) ? string.Empty : laDetailDemande.LaDemande.LIBELLEPRODUIT;
                    this.txt_Produit.Tag = laDetailDemande.LaDemande.FK_IDPRODUIT;
                    this.txt_tdem.Text = string.IsNullOrEmpty(laDetailDemande.LaDemande.LIBELLETYPEDEMANDE) ? string.Empty : laDetailDemande.LaDemande.LIBELLETYPEDEMANDE;
                    Tdem = string.IsNullOrEmpty(laDetailDemande.LaDemande.TYPEDEMANDE) ? string.Empty : laDetailDemande.LaDemande.TYPEDEMANDE;
                    this.txt_tdem.Tag = SessionObject.LstTypeDemande.FirstOrDefault(t => t.CODE == Tdem);

                    this.txtSite.IsReadOnly = true;
                    this.txtCentre.IsReadOnly = true;
                    this.Txt_ReferenceClient.IsReadOnly = true;
                    this.txt_Produit.IsReadOnly = true;
                    this.txt_tdem.IsReadOnly = true;

                    RemplireOngletClient(laDetailDemande.LeClient);
                    RemplirOngletAbonnement(laDetailDemande.Abonne);
                    RemplireOngletFacture(laDetailDemande.LstCoutDemande);
                    RemplireOngletObjetScanne(laDetailDemande.ObjetScanne);
                    IsRejeterDemande = true;
                }
                
            };
        }
        private void AfficherOuMasquerClientNonEDM(bool pValue)
        {
            try
            {
                this.Txt_Nom.Visibility = pValue ? Visibility.Visible : Visibility.Collapsed;
                this.lbl_Nom.Visibility = pValue ? Visibility.Visible : Visibility.Collapsed;

                //this.lbl_Portable.Visibility = pValue ? Visibility.Visible : Visibility.Collapsed;
                //this.Txt_Portable.Visibility = pValue ? Visibility.Visible : Visibility.Collapsed;

                //this.lbl_NumeroFixe.Visibility = pValue ? Visibility.Visible : Visibility.Collapsed;
                //this.Txt_NumeroFixe.Visibility = pValue ? Visibility.Visible : Visibility.Collapsed;

                //this.lbl_Adress.Visibility = pValue ? Visibility.Visible : Visibility.Collapsed;
                //this.Txt_Adress.Visibility = pValue ? Visibility.Visible : Visibility.Collapsed;

                //this.lbl_Email.Visibility = pValue ? Visibility.Visible : Visibility.Collapsed;
                //this.Txt_Email.Visibility = pValue ? Visibility.Visible : Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void AfficherOuMasquer(TabItem pTabItem, bool pValue)
        {
            try
            {
                pTabItem.Visibility = pValue ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        List<ServiceAccueil.CsCentre> lesCentre = new List<ServiceAccueil.CsCentre>();
        List<ServiceAccueil.CsSite> lesSite = new List<ServiceAccueil.CsSite>();
        List<int> lstIdCentre = new List<int>();
        void ChargerListDesSite()
        {
            try
            {

                if (SessionObject.LstCentre != null && SessionObject.LstCentre.Count != 0)
                {
                    lesCentre = Shared.ClasseMEthodeGenerique.RetourCentreByPerimetre(SessionObject.LstCentre, UserConnecte.listeProfilUser);
                    lesSite = Shared.ClasseMEthodeGenerique.RetourneSiteByCentre(lesCentre);
                    _listeDesCentreExistant = lesCentre;

                    foreach (ServiceAccueil.CsCentre item in lesCentre)
                        lstIdCentre.Add(item.PK_ID);
                    return;
                }
                Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient service = new Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
                service.ListeDesDonneesDesSiteAsync(false);
                service.ListeDesDonneesDesSiteCompleted += (s, args) =>
                {
                    try
                    {
                        if (args != null && args.Cancelled)
                            return;
                        SessionObject.LstCentre = args.Result;
                        if (SessionObject.LstCentre.Count != 0)
                        {
                            lesCentre = Shared.ClasseMEthodeGenerique.RetourCentreByPerimetre(SessionObject.LstCentre, UserConnecte.listeProfilUser);
                            lesSite = Shared.ClasseMEthodeGenerique.RetourneSiteByCentre(lesCentre);
                            _listeDesCentreExistant = lesCentre;

                            foreach (ServiceAccueil.CsCentre item in lesCentre)
                                lstIdCentre.Add(item.PK_ID);

                        }
                        else
                        {
                            Message.ShowInformation("Aucun site trouvé en base.", "Erreur");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Message.ShowError(ex, "Erreur");
                    }
                };
            }
            catch (Exception ex)
            {
                Message.ShowError(ex, "Erreur");
            }
        }
        public ObservableCollection<ObjDOCUMENTSCANNE> LstPiece = new ObservableCollection<ObjDOCUMENTSCANNE>();
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.OKButton.IsEnabled = false;
                this.CancelButton.IsEnabled = false ;
                if (!VerifieChampObligation()) return;


                Guid GroupeDeValidation = (Guid)this.txt_GroupeValidation.Tag;
                ValidationDemande(GroupeDeValidation);
            }
            catch (Exception ex)
            {
                this.DialogResult = false;
                Message.Show(ex.Message, Languages.txtDevis);
            }
        }
        string centre = string.Empty;
        string ordre = string.Empty;
        CsDemandeReclamation LaDemande = new CsDemandeReclamation();
        int EtapeActuelle;

        private CsDemandeReclamation GetInformationsFromScreen(CsDemandeReclamation listObjetForInsertOrUpdate)
        {
            

            if (listObjetForInsertOrUpdate == null)
            {
                listObjetForInsertOrUpdate = new CsDemandeReclamation();
                listObjetForInsertOrUpdate.LaDemande = new CsDemandeBase();
                listObjetForInsertOrUpdate.ReclamationRcl = new CsReclamationRcl();
                listObjetForInsertOrUpdate.DonneDeDemande = new List<ObjDOCUMENTSCANNE>();
                listObjetForInsertOrUpdate.LaDemande.DATECREATION = DateTime.Now;
                listObjetForInsertOrUpdate.LaDemande.USERCREATION = UserConnecte.matricule;
                listObjetForInsertOrUpdate.LaDemande.FK_IDADMUTILISATEUR = UserConnecte.PK_ID;
            }

            Galatee.Silverlight.ServiceAccueil.CsTdem leTydemande = new Galatee.Silverlight.ServiceAccueil.CsTdem();
            leTydemande = SessionObject.LstTypeDemande.FirstOrDefault(t => t.CODE == SessionObject.Enumere.DemandeReclamation);

            listObjetForInsertOrUpdate.LaDemande.ISNEW = true;

            listObjetForInsertOrUpdate.LaDemande.DATEMODIFICATION = DateTime.Now;
            listObjetForInsertOrUpdate.LaDemande.TYPEDEMANDE = (string)leTydemande.CODE;
            listObjetForInsertOrUpdate.LaDemande.FK_IDTYPEDEMANDE = (int)leTydemande.PK_ID;
            listObjetForInsertOrUpdate.LaDemande.FK_IDADMUTILISATEUR = UserConnecte.PK_ID;

            try
            {

                Guid Fk_Id_GroupValidation = new Guid();
                if (this.txt_GroupeValidation.Tag != null)
                    Guid.TryParse(this.txt_GroupeValidation.Tag.ToString(), out Fk_Id_GroupValidation);
                var sReclamationRcl = new CsReclamationRcl
                {
                    DateRetourSouhaite = Convert.ToDateTime(Dtp_DateretourSouhaite.SelectedDate),
                    DateOuverture = Convert.ToDateTime(Dtp_DateOuverture.SelectedDate),
                    DateRdv = Convert.ToDateTime(Dtp_DateRendezVous.SelectedDate),
                    NomClient = Txt_NomClient.Text,
                    NumeroTelephonePortable = Txt_Portable.Text,
                    Adresse = Txt_Adress.Text,
                    Email = Txt_Email.Text,
                    NumeroTelephoneFixe = Txt_NumeroFixe.Text,
                    ObjetReclamation = Txt_Object.Text,
                    AgentEmetteur = Txt_EtablirPar.Tag != null ? Txt_EtablirPar.Tag.ToString() : UserConnecte.matricule,

                    Client = Txt_ReferenceClient.Text,
                    NumeroReclamation = string.Empty,
                    Fk_IdTypeReclamation = ((Galatee.Silverlight.ServiceAccueil.CsTypeReclamationRcl)Cbo_TypeReclamation.SelectedItem).PK_ID,
                    LIBELLETYPERECLAMATION = ((Galatee.Silverlight.ServiceAccueil.CsTypeReclamationRcl)Cbo_TypeReclamation.SelectedItem).Libelle,
                    Fk_IdModeReception = int.Parse(((Galatee.Silverlight.ServiceAccueil.CsModeReception)Cbo_ModeReception.SelectedItem).pk_id.ToString()),
                    Fk_Id_GroupValidation = Fk_Id_GroupValidation

                };
                if (Chk_EstClient.IsChecked == true)
                {
                    sReclamationRcl.Client = Client.REFCLIENT;
                    sReclamationRcl.Fk_IdClient = Client.PK_ID;
                    sReclamationRcl.Fk_IdCentre = Client.FK_IDCENTRE;
                    sReclamationRcl.CENTRE = Client.CENTRE;

                    listObjetForInsertOrUpdate.LaDemande.ORDRE = laDetailDemande.LeClient.ORDRE;
                    listObjetForInsertOrUpdate.LaDemande.PRODUIT = laDetailDemande.Abonne.PRODUIT;
                    listObjetForInsertOrUpdate.LaDemande.FK_IDPRODUIT = laDetailDemande.Abonne.FK_IDPRODUIT;
                    listObjetForInsertOrUpdate.LaDemande.CLIENT = laDetailDemande.Abonne.CLIENT;
                    listObjetForInsertOrUpdate.LaDemande.CENTRE = laDetailDemande.Abonne.CENTRE;
                    listObjetForInsertOrUpdate.LaDemande.FK_IDCENTRE = laDetailDemande.Abonne.FK_IDCENTRE;
                }
                else
                {
                    sReclamationRcl.Client = this.Txt_ReferenceClient.Text;
                    sReclamationRcl.NomClient  = this.Txt_Nom.Text;
                    sReclamationRcl.Fk_IdClient = null;
                    sReclamationRcl.Fk_IdCentre = UserConnecte.FK_IDCENTRE;
                    sReclamationRcl.CENTRE = UserConnecte.Centre ;
                    sReclamationRcl.Ordre =this.Txt_Ordre.Text;



                    listObjetForInsertOrUpdate.LaDemande.ORDRE = this.Txt_Ordre.Text ;
                    if (this.txt_Produit.Tag != null)
                    {
                        listObjetForInsertOrUpdate.LaDemande.PRODUIT = ((CsProduit)this.txt_Produit.Tag).CODE;
                        listObjetForInsertOrUpdate.LaDemande.FK_IDPRODUIT = ((CsProduit)this.txt_Produit.Tag).PK_ID;
                    }
                    listObjetForInsertOrUpdate.LaDemande.CLIENT = this.Txt_ReferenceClient.Text;
                    listObjetForInsertOrUpdate.LaDemande.CENTRE = sReclamationRcl.CENTRE;
                    listObjetForInsertOrUpdate.LaDemande.FK_IDCENTRE = UserConnecte.FK_IDCENTRE; 
                }


                //centre = UserConnecte.Centre;
                //if (Client != null && !string.IsNullOrEmpty(Client.REFCLIENT))
                //{
                //    centre = Client.CENTRE;

                //    sReclamationRcl.Client = Client.REFCLIENT;
                //    sReclamationRcl.Fk_IdClient = Client.PK_ID;
                //    sReclamationRcl.Fk_IdCentre = Client.FK_IDCENTRE;
                //    sReclamationRcl.CENTRE = Client.CENTRE;
                //    ordre = Client.ORDRE;

                  
                //}
                //else
                //{
                //    listObjetForInsertOrUpdate.LaDemande.CENTRE = UserConnecte.Centre;
                //    listObjetForInsertOrUpdate.LaDemande.FK_IDCENTRE = UserConnecte.FK_IDCENTRE;
                //}
                if (LaDemande.ReclamationRcl != null && LaDemande.ReclamationRcl.PK_ID != null && LaDemande.ReclamationRcl.PK_ID != 0)
                    sReclamationRcl.PK_ID = LaDemande.ReclamationRcl.PK_ID;
                listObjetForInsertOrUpdate.ReclamationRcl = sReclamationRcl;
                #region Doc Scanne
                if (listObjetForInsertOrUpdate.DonneDeDemande == null) listObjetForInsertOrUpdate.DonneDeDemande = new List<ObjDOCUMENTSCANNE>();
                listObjetForInsertOrUpdate.DonneDeDemande.AddRange(LstPiece);
                #endregion

                return listObjetForInsertOrUpdate;
            }
            catch (Exception ex)
            {
                Message.ShowError(ex.Message, null);
                return null;
            }
        }
        private void EnvoyerDemandeEtapeSuivante(List<int> Listid, string idDem, string numDem, Guid idGroup)
        {
            ServiceWorkflow.WorkflowClient clientWkf = new ServiceWorkflow.WorkflowClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Workflow"));

            clientWkf.ExecuterActionSurDemandeParPkIDLigneCompleted += (wkf, wsen) =>
            {
                if (null != wsen && wsen.Cancelled)
                {
                    Message.ShowError("Echec ","Reclamation");
                    return;
                }
                if (string.Empty != wsen.Result && wsen.Result.StartsWith("ERR"))
                {
                    Message.ShowError("Echec ", "Reclamation");
                    return;
                }
                else
                {
                    Message.ShowInformation("Demande transmise avec succès", "Reclamation");

                    MiseAJourGroupSurCopieCircuit(idDem, numDem, idGroup);

                    this.DialogResult = true;
                }
            };
            clientWkf.ExecuterActionSurDemandeParPkIDLigneAsync(Listid, EtapeActuelle, SessionObject.Enumere.TRANSMETTRE, UserConnecte.matricule,
                string.Empty);
        }
        private void MiseAJourGroupSurCopieCircuit(string idDemande, string numdem, Guid idGroup)
        {

            Galatee.Silverlight.ServiceParametrage.ParametrageClient service = new Galatee.Silverlight.ServiceParametrage.ParametrageClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Parametrage"));

            service.UpdateGroupValidationCopieCircuitCompleted += (wkf, wsen) =>
            {
                if (null != wsen && wsen.Cancelled)
                {
                    Message.ShowError("Echec ", "Reclamation");
                    return;
                }
                if (!wsen.Result)
                {
                    Message.ShowError("Echec ", "Reclamation");
                    return;
                }
                else
                {

                }
            };
            service.UpdateGroupValidationCopieCircuitAsync(idDemande, numdem, idGroup);
        }

        private void ValidationDemande(Guid idgroupValidation)
        {
            try
            {
                CsDemandeReclamation _LaDemande = new CsDemandeReclamation();
                _LaDemande = GetInformationsFromScreen(null );

                //Lancer la transaction de mise a jour en base
                Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient service1 = new Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
                service1.ValiderInitReclamationCompleted += (sr, res) =>
                {
                    if (res != null && res.Cancelled)
                        return;
                    if (!string.IsNullOrEmpty(res.Result))
                    {
                        string Retour = res.Result;
                        string[] coupe = Retour.Split('.');

                        if (EtapeActuelle == 0)
                            Shared.ClasseMEthodeGenerique.InitWOrkflowToGroupValidation(coupe[0], _LaDemande.LaDemande.FK_IDCENTRE, coupe[1], idgroupValidation, _LaDemande.LaDemande.FK_IDTYPEDEMANDE);
                        else
                        {
                            List<int> Listid = new List<int>();
                            Listid.Add(LaDemande.LaDemande.PK_ID);
                            //EnvoyerDemandeEtapeSuivante(Listid);
                            EnvoyerDemandeEtapeSuivante(Listid, coupe[0], coupe[1], idgroupValidation);
                        }

                        //if (this.demande != 0)
                        //{
                        //    List<int> Listid = new List<int>();
                        //    Listid.Add(this.demande);
                        //    EnvoyerDemandeEtapeSuivante(Listid);
                        //    //Shared.ClasseMEthodeGenerique.InitWOrkflow(coupe[0], _LaDemande.LaDemande.FK_IDCENTRE, coupe[1], _LaDemande.LaDemande.FK_IDTYPEDEMANDE);
                        //}
                        //else
                        //{
                        //    this.demande = int.Parse(coupe[0]);
                        //    InitWOrkflow((coupe[0]), UserConnecte.FK_IDCENTRE, "Galatee.Silverlight.Accueil.UcInformationsReclamationAnc", coupe[1], SaveMandatement, null);
                        //}
                        //numedemande = coupe[1];
                        //Client = coupe[2];


                        List<CsReclamationRcl> leDemandeAEditer = new List<CsReclamationRcl>();
                        _LaDemande.ReclamationRcl.NumeroReclamation = coupe[1];
                        _LaDemande.ReclamationRcl.QUARTIER = centre;
                        _LaDemande.ReclamationRcl.NOMAGENTRECEPTEUR = ordre;
                        leDemandeAEditer.Add(_LaDemande.ReclamationRcl);
                        Utility.ActionDirectOrientation<ServicePrintings.CsReclamationRcl, CsReclamationRcl>(leDemandeAEditer, null, SessionObject.CheminImpression, "FicheDeTravail", "Reclamation", true);

                    }
                    //if (Closed != null)
                    //    Closed(this, new EventArgs());
                    this.DialogResult = false;
                };
                service1.ValiderInitReclamationAsync(_LaDemande);
                service1.CloseAsync();
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }
        private bool VerifieChampObligation()
        {
            try
            {
                bool ReturnValue = true;

                if (this.Chk_EstClient.IsChecked == true)
                {
                    if (laDetailDemande.LeClient == null)
                        throw new Exception("Saisir le client ");
                }
                else
                {
                    if (string.IsNullOrEmpty(this.Txt_Nom.Text))
                        throw new Exception("remplir le Nom");
                }
                #region information abonnement

                if (string.IsNullOrEmpty(this.Dtp_DateRendezVous.SelectedDate.ToString()))
                    throw new Exception("Selectionnez Date Rendez Vous");
                if (string.IsNullOrEmpty(this.Dtp_DateOuverture.SelectedDate.ToString()))
                    throw new Exception("Selectionnez Date Ouverture");
               
                //if (string.IsNullOrEmpty(this.Txt_Prenom.Text))
                //    throw new Exception("remplir le Prénom");
                if (string.IsNullOrEmpty(this.Txt_Portable.Text))
                    throw new Exception("Saisir le numero de portable");
                //if (string.IsNullOrEmpty(this.Txt_Object.Text))
                //    throw new Exception("remplir l Object");
                if (Cbo_ModeReception.SelectedItem == null)
                    throw new Exception("Selectionnez Mode Reception ");
                if (Cbo_TypeReclamation.SelectedItem == null)
                    throw new Exception("Selectionnez Type Reclamation ");
                //if (this.btn_GroupeValidation.Tag != null)
                //    throw new Exception("Selectionnez Type Reclamation ");

                //if (string.IsNullOrEmpty(this.txt.Text))
                //        throw new Exception("remplir referend plomgs ");

                //if (((CsProduit)Cbo_Produit.SelectedItem).CODE != SessionObject.Enumere.ElectriciteMT)
                //{
                //    if (string.IsNullOrEmpty(this.txt_Reglage.Text))
                //        throw new Exception("Selectionnez le calibre ");
                //}
                #endregion

                if (this.txt_GroupeValidation.Tag == null)
                    throw new Exception("Selectionnez groupe destinataire");

                return ReturnValue;

            }
            catch (Exception ex)
            {
                // this.BtnTRansfert.IsEnabled = true;
                this.OKButton.IsEnabled = true;
                Message.ShowInformation(ex.Message, "Fraude");
                return false;
            }

        }
        //private void ValiderInitialisation(CsDemande demandedevis, bool IsTransmetre)
        //{

        //    try
        //    {
        //        if (!VerifieChampObligatioire()) return;

        //        demandedevis = GetDemandeDevisFromScreen(demandedevis, false);
        //        this.DialogResult = true;
        //        if (demandedevis != null)
        //        {
        //            demandedevis.LaDemande.MATRICULE = UserConnecte.matricule;
                     
        //            Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient client = new Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient(Utility.Protocole(), Utility.EndPoint("Accueil"));
        //            client.CreeDemandeCompleted += (ss, b) =>
        //            {
        //                if (b.Cancelled || b.Error != null)
        //                {
        //                    string error = b.Error.Message;
        //                    Message.ShowError(error, Silverlight.Resources.Devis.Languages.txtDevis);
        //                    return;
        //                }
        //                if (b.Result != null)
        //                {
        //                    List<CsDemandeBase> leDemandeAEditer = new List<CsDemandeBase>();
        //                    demandedevis.LaDemande.NOMCLIENT = demandedevis.LeClient.NOMABON;
        //                    demandedevis.LaDemande.LIBELLETYPEDEMANDE = txt_tdem.Text;
        //                    demandedevis.LaDemande.NUMDEM = b.Result.NUMDEM; ;
        //                    demandedevis.LaDemande.LIBELLEPRODUIT = this.txt_Produit.Text;
        //                    demandedevis.LaDemande.LIBELLE = "Imprimé le " + DateTime.Now + " sur le poste " + SessionObject.LePosteCourant.NOMPOSTE + " par " + UserConnecte.nomUtilisateur + "(" + UserConnecte.matricule + ") du centre " + UserConnecte.LibelleCentre;
        //                    leDemandeAEditer.Add(demandedevis.LaDemande);
        //                    Utility.ActionDirectOrientation<ServicePrintings.CsDemandeBase, CsDemandeBase>(leDemandeAEditer, null, SessionObject.CheminImpression, "AccuseRecption", "Accueil", true);

        //                    Message.ShowInformation("La demande a été créée avec succès. Numéro de votre demande : " + b.Result.NUMDEM,
        //                    Silverlight.Resources.Devis.Languages.txtDevis);
        //                }
        //            };
        //            client.CreeDemandeAsync(demandedevis, IsTransmetre);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Message.ShowError("Une erreur s'est produite à la validation ", "ValiderDemandeInitailisation");
        //    }
        //}


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        void Translate()
        {

        }
         
        private void ChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        private void Cbo_Centre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btn_transmetre_Click(object sender, RoutedEventArgs e)
        {
        }
        private void VerifieExisteDemande(CsClient leClient)
        {

            try
            {
                if (!string.IsNullOrEmpty(Txt_ReferenceClient.Text) && Txt_ReferenceClient.Text.Length == SessionObject.Enumere.TailleClient)
                {
                    string OrdreMax = string.Empty;
                    AcceuilServiceClient service = new AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
                    service.RetourneDemandeClientTypeCompleted += (s, args) =>
                    {
                        if (args != null && args.Cancelled)
                            return;
                        if (args.Result != null)
                        {
                            if (args.Result.DATEFIN  == null  && args.Result.ISSUPPRIME != true)
                            {
                                Message.ShowInformation ("Il existe une demande numero " + args.Result.NUMDEM + " sur ce client", "Accueil");
                                prgBar.Visibility = System.Windows.Visibility.Collapsed;
                                return;
                            }
                        }
                        ChargeDetailDEvis(leClient);
                    };
                    service.RetourneDemandeClientTypeAsync(leClient);
                    service.CloseAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void btn_RechercheClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                prgBar.Visibility = System.Windows.Visibility.Visible;
                
                if (Txt_ReferenceClient.Text.Length == SessionObject.Enumere.TailleClient && 
                    Tdem != SessionObject.Enumere.RemboursementAvance )
                    ChargerClientFromReference(this.Txt_ReferenceClient.Text);
                else
                {
                    if (Txt_ReferenceClient.Text.Length == SessionObject.Enumere.TailleClient &&
                        Tdem == SessionObject.Enumere.RemboursementAvance)
                    {
                        ChargerClientFromReference(this.Txt_ReferenceClient.Text, this.Txt_Ordre.Text);
                    }
                    else
                    {
                        Message.Show("La reference saisie n'est pas correcte", "Infomation");
                        prgBar.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
            catch (Exception ex)
            {
                Message.ShowInformation(ex.Message, "Demande");
            }
        }
        private void RemplireOngletObjetScanne(List<ObjDOCUMENTSCANNE> _LstDocumentScanne)
        {
            try
            {
                #region DocumentScanne
                ctrl = new Galatee.Silverlight.Shared.UcFichierJoint(_LstDocumentScanne, false);
                Vwb.Stretch = Stretch.None;
                Vwb.Child = ctrl;
                #endregion
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void RemplireOngletAdresse(CsAg _LeAdresse)
        {
            try
            {
                if (_LeAdresse != null)
                {

                    this.tab3_txt_NomClientBrt.Text = string.IsNullOrEmpty(_LeAdresse.NOMP) ? string.Empty : _LeAdresse.NOMP;
                    this.tab3_txt_LibelleCommune.Text = string.IsNullOrEmpty(_LeAdresse.LIBELLECOMMUNE) ? string.Empty : _LeAdresse.LIBELLECOMMUNE;
                    this.tab3_txt_LibelleQuartier.Text = string.IsNullOrEmpty(_LeAdresse.LIBELLEQUARTIER) ? string.Empty : _LeAdresse.LIBELLEQUARTIER;
                    this.tab3_txt_Secteur.Text = string.IsNullOrEmpty(_LeAdresse.LIBELLESECTEUR) ? string.Empty : _LeAdresse.LIBELLESECTEUR;
                    this.tab3_txt_NumRue.Text = string.IsNullOrEmpty(_LeAdresse.RUE) ? string.Empty : _LeAdresse.RUE;

                    this.tab3_txt_etage.Text = string.IsNullOrEmpty(_LeAdresse.ETAGE) ? string.Empty : _LeAdresse.ETAGE;
                    this.tab3_txt_NumLot.Text = string.IsNullOrEmpty(_LeAdresse.CADR) ? string.Empty : _LeAdresse.CADR;

                    this.tab3_txt_Telephone.Text = string.IsNullOrEmpty(_LeAdresse.TELEPHONE) ? string.Empty : _LeAdresse.TELEPHONE;
                    this.tab3_txt_OrdreTour.Text = string.IsNullOrEmpty(_LeAdresse.ORDTOUR) ? string.Empty : _LeAdresse.ORDTOUR;
                    this.tab3_txt_tournee.Text = string.IsNullOrEmpty(_LeAdresse.TOURNEE) ? string.Empty : _LeAdresse.TOURNEE;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void RemplireOngletClient(CsClient _LeClient)
        {
            try
            {
                if (_LeClient != null)
                {

                    this.Txt_NomClient.Text = (string.IsNullOrEmpty(_LeClient.NOMABON) ? string.Empty : _LeClient.NOMABON);
                    //this.Txt_Telephone1.Text = string.IsNullOrEmpty(_LeClient.TELEPHONE) ? string.Empty : _LeClient.TELEPHONE;
                    //this.tab12_txt_addresse.Text = string.IsNullOrEmpty(_LeClient.ADRMAND1) ? string.Empty : _LeClient.ADRMAND1;
                    //this.tab12_txt_addresse2.Text = string.IsNullOrEmpty(_LeClient.ADRMAND2) ? string.Empty : _LeClient.ADRMAND2;
                    //this.txt_NINA.Text = string.IsNullOrEmpty(_LeClient.NUMEROIDCLIENT) ? string.Empty : _LeClient.NUMEROIDCLIENT;
                    this.tab12_Txt_LibelleCodeConso.Text = string.IsNullOrEmpty(_LeClient.LIBELLECODECONSO) ? string.Empty : _LeClient.LIBELLECODECONSO;
                    this.tab12_Txt_LibelleCategorie.Text = string.IsNullOrEmpty(_LeClient.LIBELLECATEGORIE) ? string.Empty : _LeClient.LIBELLECATEGORIE;
                    this.tab12_Txt_LibelleEtatClient.Text = string.IsNullOrEmpty(_LeClient.LIBELLERELANCE) ? string.Empty : _LeClient.LIBELLERELANCE;
                    this.tab12_Txt_LibelleTypeClient.Text = string.IsNullOrEmpty(_LeClient.LIBELLENATURECLIENT) ? string.Empty : _LeClient.LIBELLENATURECLIENT;
                    this.tab12_Txt_Nationnalite.Text = string.IsNullOrEmpty(_LeClient.LIBELLENATIONALITE) ? string.Empty : _LeClient.LIBELLENATIONALITE;
                    this.tab12_Txt_Datecreate.Text = string.IsNullOrEmpty(_LeClient.DATECREATION.ToString()) ? string.Empty : Convert.ToDateTime(_LeClient.DATECREATION).ToShortDateString();
                    //this.tab12_Txt_DateModif.Text = string.IsNullOrEmpty(_LeClient.DATEMODIFICATION.ToString()) ? string.Empty : Convert.ToDateTime(_LeClient.DATEMODIFICATION).ToShortDateString();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void RemplirOngletAbonnement(CsAbon  _LeAbon)
        {
            if (_LeAbon != null)
            {
                this.Txt_CodeTarif.Text = !string.IsNullOrEmpty(_LeAbon.TYPETARIF) ? _LeAbon.TYPETARIF : string.Empty;
                this.Txt_CodePussanceSoucrite.Text = !string.IsNullOrEmpty(_LeAbon.PUISSANCE.Value.ToString()) ? _LeAbon.PUISSANCE.Value.ToString() : string.Empty;

                if (_LeAbon.PUISSANCE != null)
                    this.Txt_CodePussanceSoucrite.Text = Convert.ToDecimal(_LeAbon.PUISSANCE.ToString()).ToString("N2");
                if (_LeAbon.PUISSANCEUTILISEE != null)
                    this.Txt_CodePuissanceUtilise.Text = Convert.ToDecimal(_LeAbon.PUISSANCEUTILISEE.Value).ToString("N2");

                this.Txt_CodeForfait.Text = string.IsNullOrEmpty(_LeAbon.FORFAIT) ? string.Empty : _LeAbon.FORFAIT;
                this.Txt_LibelleForfait.Text = string.IsNullOrEmpty(_LeAbon.LIBELLEFORFAIT) ? string.Empty : _LeAbon.LIBELLEFORFAIT;

                this.Txt_CodeTarif.Text = string.IsNullOrEmpty(_LeAbon.TYPETARIF) ? string.Empty : _LeAbon.TYPETARIF;
                this.Txt_LibelleTarif.Text = !string.IsNullOrEmpty(_LeAbon.LIBELLETARIF) ? _LeAbon.LIBELLETARIF : string.Empty;

                this.Txt_CodeFrequence.Text = string.IsNullOrEmpty(_LeAbon.PERFAC) ? string.Empty : _LeAbon.PERFAC;
                this.Txt_LibelleFrequence.Text = !string.IsNullOrEmpty(_LeAbon.LIBELLEFREQUENCE) ? _LeAbon.LIBELLEFREQUENCE : string.Empty;

                this.Txt_CodeMoisIndex.Text = string.IsNullOrEmpty(_LeAbon.MOISREL) ? string.Empty : _LeAbon.MOISREL;
                this.Txt_LibelleMoisIndex.Text = !string.IsNullOrEmpty(_LeAbon.LIBELLEMOISIND) ? _LeAbon.LIBELLEMOISIND : string.Empty;

                this.Txt_CodeMoisFacturation.Text = string.IsNullOrEmpty(_LeAbon.MOISFAC) ? string.Empty : _LeAbon.MOISFAC;
                this.Txt_LibMoisFact.Text = !string.IsNullOrEmpty(_LeAbon.LIBELLEMOISFACT) ? _LeAbon.LIBELLEMOISFACT : string.Empty;

                this.Txt_DateAbonnement.Text = (_LeAbon.DABONNEMENT == null) ?string.Empty  : Convert.ToDateTime(_LeAbon.DABONNEMENT.Value).ToShortDateString();
            }
        }

        private void RemplireOngletFacture(List<CsDemandeDetailCout>  _LesFactClient)
        {
            try
            {
                if (_LesFactClient != null && _LesFactClient.Count != 0)
                {
                    _LesFactClient.ForEach(t => t.MONTANTTTC = t.MONTANTHT + t.MONTANTTAXE);
                    this.LsvFacture.ItemsSource = null;
                    this.LsvFacture.ItemsSource = _LesFactClient;
                    this.Txt_TotalHt.Text = _LesFactClient.Sum(t => t.MONTANTHT).Value.ToString(SessionObject.FormatMontant);
                    this.Txt_totalTaxe .Text = _LesFactClient.Sum(t => t.MONTANTTAXE ).Value .ToString(SessionObject.FormatMontant );
                    this.Txt_TotalTTC.Text = _LesFactClient.Sum(t => t.MONTANTTTC).Value.ToString(SessionObject.FormatMontant);
                    AfficherOuMasquer(tabItemCompte, true );
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void Cbo_Produit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbo_typedoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtSite_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void txtCentre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ChargerClientFromReference(string ReferenceClient)
        {
            try
            {
                Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient service = new Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
                service.RetourneClientByReferenceAsync(ReferenceClient, lstIdCentre);
                service.RetourneClientByReferenceCompleted += (s, args) =>
                {
                    if ((args != null && args.Cancelled) || (args.Error != null))
                        return;
                  
                    if (args.Result != null && args.Result.Count > 0)
                    {
                        if (args.Result != null && args.Result.Count > 1)
                        {
                            List<object> _Listgen = ClasseMEthodeGenerique.RetourneListeObjet(args.Result);
                            Galatee.Silverlight.MainView.UcListeGenerique ctr = new Galatee.Silverlight.MainView.UcListeGenerique(_Listgen, "CENTRE", "LIBELLESITE", "Liste des site");
                            ctr.Show();
                            ctr.Closed += new EventHandler(galatee_OkClickedChoixClient);
                        }
                        else
                        {
                            CsClient leClient = args.Result.First();
                            leClient.TYPEDEMANDE = Tdem;
                            VerifieExisteDemande(leClient);
                        }
                    }
                    else
                    {
                        Message.ShowError("Aucun client correspondant à ces critères n'a été trouvé", "Demande");
                        prgBar.Visibility = System.Windows.Visibility.Collapsed;
                        return;
                    }
                };
                service.CloseAsync();

            }
            catch (Exception)
            {
                prgBar.Visibility = System.Windows.Visibility.Collapsed;
                Message.ShowError("Erreur au chargement des données", "Demande");
            }
        }


        private void ChargerClientFromReference(string ReferenceClient,string Ordre)
        {
            try
            {
                Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient service = new Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
                service.RetourneClientByReferenceOrdreCompleted += (s, args) =>
                {
                    if ((args != null && args.Cancelled) || (args.Error != null))
                        return;
                    if (args.Result != null && args.Result.Count > 0)
                    {
                        if (args.Result != null && args.Result.Count > 1)
                        {
                            List<object> _Listgen = ClasseMEthodeGenerique.RetourneListeObjet(args.Result);
                            Galatee.Silverlight.MainView.UcListeGenerique ctr = new Galatee.Silverlight.MainView.UcListeGenerique(_Listgen, "CENTRE", "LIBELLESITE", "Liste des site");
                            ctr.Show();
                            ctr.Closed += new EventHandler(galatee_OkClickedChoixClient);
                        }
                        else 
                        {
                            CsClient leClient = args.Result.First();
                            leClient.TYPEDEMANDE = Tdem;
                            VerifieExisteDemande(leClient);
                        }
                    }
                    else
                    {
                        Message.ShowError("Aucun client correspondant à ces critères n'a été trouvé", "Demande");
                        prgBar.Visibility = System.Windows.Visibility.Collapsed;
                        return;
                    }
                };
                service.RetourneClientByReferenceOrdreAsync (ReferenceClient,Ordre , lstIdCentre);
                service.CloseAsync();

            }
            catch (Exception)
            {
                prgBar.Visibility = System.Windows.Visibility.Collapsed;
                Message.ShowError("Erreur au chargement des données", "Demande");
            }
        }

        private void galatee_OkClickedChoixClient(object sender, EventArgs e)
        {
            Galatee.Silverlight.MainView.UcListeGenerique ctrs = sender as Galatee.Silverlight.MainView.UcListeGenerique;
            if (ctrs.isOkClick)
            {
               CsClient  _UnClient = (CsClient)ctrs.MyObject;
               _UnClient.TYPEDEMANDE = Tdem;
               VerifieExisteDemande(_UnClient);
            }
        }

        private List<CsClient> DistinctSiteClient(List<CsClient> lstClient)
        {
            try
            {
                List<CsClient> lstCentreDistClientOrdreProduit = new List<CsClient>();
                var lstCentreDistnct = lstClient.Select(t => new { t.LIBELLESITE ,t.FK_IDCENTRE , t.CENTRE ,t.REFCLIENT,t.PRODUIT   }).Distinct().ToList();
                foreach (var item in lstCentreDistnct)
                {
                    CsClient leClient = new CsClient() 
                    {
                      FK_IDCENTRE = item.FK_IDCENTRE ,
                      CENTRE = item.CENTRE ,
                      REFCLIENT = item.REFCLIENT ,
                      PRODUIT = item.PRODUIT 
                    };
                    lstCentreDistClientOrdreProduit.Add(leClient);
                }
                return lstCentreDistClientOrdreProduit;
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        private void ChargeDetailDEvis(CsClient leclient)
        {

            try
            {
                leclient.TYPEDEMANDE = Tdem;
                Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient client = new Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
                client.ChargerDetailClientAsync(leclient);
                client.ChargerDetailClientCompleted += (ssender, args) =>
                {
                    prgBar.Visibility = System.Windows.Visibility.Collapsed;

                    if (args.Cancelled || args.Error != null)
                    {
                        string error = args.Error.Message;
                        Message.ShowError(error, Silverlight.Resources.Devis.Languages.txtDevis);
                        return;
                    }
                    if (args.Result == null)
                    {
                        Message.ShowError(Silverlight.Resources.Devis.Languages.AucunesDonneesTrouvees, Silverlight.Resources.Devis.Languages.txtDevis);
                        return;
                    }
                    else
                    {

                        laDetailDemande = new CsDemande();
                        laDetailDemande = args.Result;
                        this.txtSite.Text = string.IsNullOrEmpty(laDetailDemande.LeClient.LIBELLESITE) ? string.Empty : laDetailDemande.LeClient.LIBELLESITE;
                        this.txtCentre.Text = string.IsNullOrEmpty(laDetailDemande.LeClient.LIBELLECENTRE) ? string.Empty : laDetailDemande.LeClient.LIBELLECENTRE;
                        this.txt_Produit.Text = string.IsNullOrEmpty(laDetailDemande.Abonne.LIBELLEPRODUIT) ? string.Empty : laDetailDemande.Abonne.LIBELLEPRODUIT;
                        this.txt_Produit.Tag  = laDetailDemande.Abonne.FK_IDPRODUIT ;
                        this.txt_tdem.Text = SessionObject.LstTypeDemande.FirstOrDefault(t => t.CODE == Tdem).LIBELLE;
                        txt_tdem.Tag = SessionObject.LstTypeDemande.FirstOrDefault(t => t.CODE == Tdem);
                        this.Txt_Ordre.Text = laDetailDemande.LeClient.ORDRE;
                       
                        RemplireOngletClient(laDetailDemande.LeClient);
                        RemplirOngletAbonnement(laDetailDemande.Abonne);
                        RemplireOngletAdresse(laDetailDemande.Ag);
                    }
                };
            }
            catch (Exception ex)
            {
                prgBar.Visibility = System.Windows.Visibility.Collapsed;
                Message.ShowError("Erreur au chargement des donnéés", "Demande");
            }
        }
       //private void ChargerCompteDeResiliation(CsClient _UnClient)
       // {

       //     AcceuilServiceClient client = new AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
       //     client.ChargerCompteDeResiliationCompleted += (s, args) =>
       //     {
       //         if (args != null && args.Cancelled)
       //             return;
       //         if (args.Result == null || args.Result.Count == 0)
       //         {
       //             Message.ShowInformation("Ce client n'existe pas", "RetourneListeFactureNonSolde");
       //             return;
       //         }
       //         List<CsLclient> lstFactureDuClient = args.Result;
       //         lstFactureDuClient.ForEach(t => t.REFEM = ClasseMEthodeGenerique.FormatPeriodeMMAAAA(t.REFEM));
       //         if (lstFactureDuClient != null && lstFactureDuClient.Count != 0)
       //         {
       //             AfficherOuMasquer(tabItemCompte, true);
       //             Txt_TotalHt.Visibility = System.Windows.Visibility.Collapsed ;
       //             Txt_totalTaxe.Visibility = System.Windows.Visibility.Collapsed ;
       //             lbl_total.Content = "Solde client";
       //             LsvFacture.ItemsSource = lstFactureDuClient;
       //             Txt_TotalTTC.Text = lstFactureDuClient.Sum(t => t.SOLDEFACTURE).Value.ToString(SessionObject.FormatMontant);
       //         }
       //     };
       //     client.ChargerCompteDeResiliationAsync(_UnClient);
       //     client.CloseAsync();
        
       // }
       private void ChargerFraisParticipation(CsClient _UnClient)
       {

           AcceuilServiceClient client = new AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
           client.ChargerFraisParticipationCompleted += (s, args) =>
           {
               if (args != null && args.Cancelled)
                   return;
               if (args.Result == null || args.Result.Count == 0)
               {
                   Message.ShowInformation("Ce client n'a pas de frais de participation a rembourser", "Info");
                   return;
               }
               List<CsLclient> lstFactureDuClient = args.Result;
               lstFactureDuClient.ForEach(t => t.REFEM = ClasseMEthodeGenerique.FormatPeriodeMMAAAA(t.REFEM));
               if (lstFactureDuClient != null && lstFactureDuClient.Count != 0)
               {
                   AfficherOuMasquer(tabItemCompte, true);
                   LsvFacture.ItemsSource = lstFactureDuClient;
               }
           };
           client.ChargerFraisParticipationAsync(_UnClient);
           client.CloseAsync();

       }
       private void RemplirListeDesReglageExistant()
       {
           try
           {
               if (SessionObject.LstReglageCompteur != null && SessionObject.LstReglageCompteur.Count != 0)
               {
                   _listeDesReglageCompteurExistant = SessionObject.LstReglageCompteur;
                   return;
               }
               AcceuilServiceClient service = new AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
               service.ChargerReglageCompteurCompleted += (s, args) =>
               {
                   if (args != null && args.Cancelled)
                       return;
                   SessionObject.LstReglageCompteur = args.Result;
                   _listeDesReglageCompteurExistant = SessionObject.LstReglageCompteur;

               };
               service.ChargerReglageCompteurAsync();
               service.CloseAsync();
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       private void Btn_Reglage_Click(object sender, RoutedEventArgs e)
       {
           try
           {
               if (this.txt_Produit.Tag  != null)
               {
                   var UcListReglage = new Galatee.Silverlight.Accueil.UcListeReglageCompteur(_listeDesReglageCompteurExistant.Where(t => t.FK_IDPRODUIT ==int.Parse(this.txt_Produit.Tag.ToString())).ToList());
                   UcListReglage.Closed += new EventHandler(UcListReglage_Closed);
                   this.IsEnabled = false;
                   UcListReglage.Show();
               }

           }
           catch (Exception ex)
           {
               Message.ShowError(ex.Message, Languages.txtDevis);
           }
       }
       private void UcListReglage_Closed(object sender, EventArgs e)
       {
           try
           {
               this.IsEnabled = true;
               UcListeReglageCompteur ctrs = sender as UcListeReglageCompteur;
               if (ctrs.isOkClick)
               {
                   if (ctrs.leReglageSelect != null)
                   {
                       this.txt_Reglage.Text = ctrs.leReglageSelect.LIBELLE;
                       this.txt_Reglage.Tag = ctrs.leReglageSelect.PK_ID;
                       this.Btn_Reglage.Tag = ctrs.leReglageSelect.CODE;
                   }
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       private void ChargerModeReception()
       {
           try
           {
               Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient client = new Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
               client.SelectAllModeReceptionCompleted += (ssender, args) =>
               {
                   if (args.Cancelled || args.Error != null)
                   {
                       string error = args.Error.Message;
                       Message.ShowError(error, "");
                       return;
                   }
                   if (args.Result == null)
                   {
                       //Message.ShowError(Languages.msgErreurChargementDonnees, Languages.Parametrage);
                       Message.ShowError(Galatee.Silverlight.Resources.Devis.Languages.msgErreurChargementDonnees, "");
                       return;
                   }
                   if (args.Result != null)
                   {
                       Cbo_ModeReception.ItemsSource = null;
                       Cbo_ModeReception.DisplayMemberPath = "Libelle";
                       Cbo_ModeReception.SelectedValuePath = "PK_ID";
                       Cbo_ModeReception.ItemsSource = args.Result;
                       //ChargeDonneDemande(demande);

                   }
               };
               client.SelectAllModeReceptionAsync();
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       private void ChargerTypeReclamation()
       {
           try
           {
               Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient client = new Galatee.Silverlight.ServiceAccueil.AcceuilServiceClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Accueil"));
               client.SelectAllTypeReclamationRclCompleted += (ssender, args) =>
               {
                   if (args.Cancelled || args.Error != null)
                   {
                       string error = args.Error.Message;
                       Message.ShowError(error, "");
                       return;
                   }
                   if (args.Result == null)
                   {
                       //Message.ShowError(Languages.msgErreurChargementDonnees, Languages.Parametrage);
                       Message.ShowError(Galatee.Silverlight.Resources.Devis.Languages.msgErreurChargementDonnees, "");
                       return;
                   }
                   if (args.Result != null)
                   {
                       Cbo_TypeReclamation.ItemsSource = null;
                       Cbo_TypeReclamation.DisplayMemberPath = "Libelle";
                       Cbo_TypeReclamation.SelectedValuePath = "PK_ID";
                       Cbo_TypeReclamation.ItemsSource = args.Result;
                       //ChargeDonneDemande(demande);

                   }
               };
               client.SelectAllTypeReclamationRclAsync();
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       List<Galatee.Silverlight.ServiceParametrage.CsGroupeValidation> lesGroupeValidation = new List<ServiceParametrage.CsGroupeValidation>();
       private void RemplirGroupeValidationDepannage()
       {
           try
           {

               Galatee.Silverlight.ServiceParametrage.ParametrageClient service = new Galatee.Silverlight.ServiceParametrage.ParametrageClient(Utility.ProtocoleFacturation(), Utility.EndPoint("Parametrage"));
               service.SelectAllGroupeValidationSpecifiqueCompleted += (s, args) =>
               {
                   if (args != null && args.Cancelled)
                       return;
                   lesGroupeValidation = args.Result;
               };
               service.SelectAllGroupeValidationSpecifiqueAsync(1);
               service.CloseAsync();
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       private void btn_GroupeValidation_Click_1(object sender, RoutedEventArgs e)
       {
           if (lesGroupeValidation != null)
           {

               List<object> _LstObj = new List<object>();
               _LstObj = ClasseMEthodeGenerique.RetourneListeObjet(lesGroupeValidation);
               Dictionary<string, string> _LstColonneAffich = new Dictionary<string, string>();
               _LstColonneAffich.Add("GROUPENAME", "GROUPE DESTINATAIRE");

               List<object> obj = Shared.ClasseMEthodeGenerique.RetourneListeObjet(_LstObj);
               MainView.UcListeGenerique ctrl = new MainView.UcListeGenerique(obj, _LstColonneAffich, false, "Groupe");
               ctrl.Closed += new EventHandler(galatee_OkClickedBatch);
               ctrl.Show();
           }
       }

       void galatee_OkClickedBatch(object sender, EventArgs e)
       {
           Galatee.Silverlight.MainView.UcListeGenerique ctrs = sender as Galatee.Silverlight.MainView.UcListeGenerique;
           if (ctrs.isOkClick)
           {
               Galatee.Silverlight.ServiceParametrage.CsGroupeValidation _LesGroupeValiadation = ctrs.MyObject as Galatee.Silverlight.ServiceParametrage.CsGroupeValidation;
               this.txt_GroupeValidation.Text = string.IsNullOrEmpty(_LesGroupeValiadation.GROUPENAME) ? string.Empty : _LesGroupeValiadation.GROUPENAME;
               this.txt_GroupeValidation.Tag = _LesGroupeValiadation.PK_ID;
           }
       }

       //private void NewfrmPj_CallBack(object sender, CustumEventArgs e)
       //{
       //    string test = e.Data as string;
       //}

       private void Chk_EstClient_Checked(object sender, RoutedEventArgs e)
       {
           if (Chk_EstClient.IsChecked == true)
           {
               AfficherOuMasquerClientNonEDM(false);
               Txt_ReferenceClient.Text = string.Empty;
               this.btn_RechercheClient.IsEnabled = true;

               AfficherOuMasquer(tabItemCompte, true);
               AfficherOuMasquer(tabItemClient, true);
               AfficherOuMasquer(tabItemAdrss, true);
               AfficherOuMasquer(tabItemAbon, true);
               AfficherOuMasquer(tab_demande, true);
           }
           else
           {
               AfficherOuMasquerClientNonEDM(true);
               Txt_ReferenceClient.Text = "G" + UserConnecte.matricule + System.DateTime.Today.Month.ToString("00") + System.DateTime.Today.Day.ToString("00");
               this.btn_RechercheClient.IsEnabled = false;
               this.txtCentre.Text = SessionObject.LstCentre.FirstOrDefault(y => y.CODE == UserConnecte.Centre).LIBELLE;
               this.txtCentre.Tag  = SessionObject.LstCentre.FirstOrDefault(y => y.CODE == UserConnecte.Centre).PK_ID ;
               this.txtSite.Text = SessionObject.LstCentre.FirstOrDefault(y => y.CODE == UserConnecte.Centre).LIBELLESITE;
               this.txtSite.Tag  = SessionObject.LstCentre.FirstOrDefault(y => y.CODE == UserConnecte.Centre).FK_IDCODESITE ;
               this.txt_Produit.Text = SessionObject.ListeDesProduit.FirstOrDefault(y => y.CODE == "03").LIBELLE;
               this.txt_Produit.Tag = SessionObject.ListeDesProduit.FirstOrDefault(y => y.CODE == "03");

               this.Txt_Ordre.Text = "01";
               AfficherOuMasquer(tabItemCompte, false);
               AfficherOuMasquer(tabItemClient, false);
               AfficherOuMasquer(tabItemAdrss, false);
               AfficherOuMasquer(tabItemAbon, false);
               AfficherOuMasquer(tab_demande, true);

           }
       }

       private void Chk_EstReclamation_Checked(object sender, RoutedEventArgs e)
       {
           if (Chk_EstReclamation.IsChecked == true)
               Btn_RechercherReclamation.IsEnabled = true;
           else
               Btn_RechercherReclamation.IsEnabled = false;
       }

       private void Button_Click(object sender, RoutedEventArgs e)
       {

       }

       private void NewButton1_Click(object sender, RoutedEventArgs e)
       {

       }

    }
}


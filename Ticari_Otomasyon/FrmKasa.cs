using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        public string ad;

        void musteriHareketleri()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec MusteriHareketler", bgl.baglanti());
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }

        void firmaHareketleri()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Exec FirmaHareketler", bgl.baglanti());
            da2.Fill(dt2);
            gridControl1.DataSource = dt2;
        }

        private void FrmKasa_Load_1(object sender, EventArgs e)
        {
            LblAktifKullanici.Text = ad;

            musteriHareketleri();
            firmaHareketleri();

            //Toplam Tutarı Hesaplama
            SqlCommand komut1 = new SqlCommand("Select Sum(TUTAR) From TBL_FATURADETAY", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblToplamTutar.Text = dr1[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            //Son Ayın Faturaları
            SqlCommand komut2 = new SqlCommand("Select (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) from TBL_GIDERLER order by ID asc", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblOdemeler.Text = dr2[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            //Son Ayın Personel Maaşları
            SqlCommand komut3 = new SqlCommand("Select Maaslar from TBL_GIDERLER order by ID asc", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblPersonelMaaslari.Text = dr3[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            //Toplam Müşteri Sayısı
            SqlCommand komut4 = new SqlCommand("Select Count(*) From TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                LblMusteriSayisi.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Firma Sayısı
            SqlCommand komut5 = new SqlCommand("Select Count(*) From TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                LblFirmaSayisi.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Firma Şehir Sayısı
            SqlCommand komut6 = new SqlCommand("Select Count(Distinct(IL)) From TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                LblFirmaSehirSayisi.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Müşteri Şehir Sayısı
            SqlCommand komut7 = new SqlCommand("Select Count(Distinct(IL)) From TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                LblMusteriSehirSayisi.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Personel Sayısı
            SqlCommand komut8 = new SqlCommand("Select Count(*) From TBL_PERSONELLER", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                LblPersonelSayisi.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam Stok Sayısı
            SqlCommand komut9 = new SqlCommand("Select Sum(ADET) From TBL_URUNLER", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                LblStokSayisi.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}

/// <summary>
///  Método que popula o array bidimencional (aMapaCapi), 
/// </summary>
/// <param name="txtValorMedia">Valor pretendido de lucro por subperíodo</param>
/// <param name="txtNumSB">Número de SB na tabela</param>
/// <param name="cboNumGP">Número de grupos na série</param>
/// <param name="aMapaCapi">Array bidimencional com a tabela final pronta no Load do Form</param>
/// <param name="lvMapaCapi">ListView que serve como Display para o array acima e usado, também, 
/// como alternativa à uma tabela de Banco de Dados</param>

public static void MontaMapaCapi(TextBox txtNumSB, TextBox txtValorMedia, 
								 ComboBox cboNumGP, ref string[,] aMapaCapi, 
								 ListView lvMapaCapi)
{
	decimal vIndice = 0.00M;
	decimal vDebito = 0.00M;
	decimal vCredito = 0.00M;
	decimal vSaldo = 0.00M;
	decimal vSoma = 0.00M;
	int vNumSB = Convert.ToInt32(txtNumSB.Text);
	int vNumDigito = Convert.ToInt32(cboNumGP.Text);
	decimal vMedia = Convert.ToDecimal(txtValorMedia.Text);
	//decimal vPerc;

	
 	if (String.IsNullOrEmpty(txtNumSB.Text) || String.IsNullOrEmpty(txtValorMedia.Text) || String.IsNullOrEmpty(cboNumGP.Text))
            {
		CSharpUtil.Util.MsgErro("CAMPOS MÉDIA e NÚMERO de DÍGITOS SÃO OBRIGATÓRIOS!");
		Application.Exit();
	}
	else
	{
		lvMapaCapi.Items.Clear();

		int k = 0;
		for (int i = 0; i < vNumSB; i++)
		{
			k++;
			vIndice = ((vMedia * k) + vSoma) / (18 - vNumDigito);
			vDebito = vIndice * vNumDigito;
			vSoma = vSoma + vDebito;
			vCredito = vIndice * 18;
			vSaldo = vCredito - vSoma;
			vMedia = vSaldo / k;
			// vPerc = (vSaldo*100)/vCG

			aMapaCapi[i, 0] = k.ToString();
			aMapaCapi[i, 1] = k.ToString();
			aMapaCapi[i, 2] = vIndice.ToString();
			aMapaCapi[i, 3] = vDebito.ToString();
			aMapaCapi[i, 4] = vSoma.ToString();
			aMapaCapi[i, 5] = vCredito.ToString();
			aMapaCapi[i, 6] = vSaldo.ToString();
			aMapaCapi[i, 7] = vMedia.ToString();


			// Este ListView serve apenas como Display, pois o 
			// Array aMapaCapi tem os mesmos dados e está em mémória
			// de onde são utilizados durante a execução do projeto.
			// Na otimização eliminar o Array e utilizar apenas a ListView!
			lvMapaCapi.Items.Add(new ListViewItem(new string[] { 
								 k.ToString(),
								 k.ToString(),
								 vIndice.ToString("0.00000"),
								 vDebito.ToString("0.00000"),
								 vSoma.ToString("0.00000"),
								 vCredito.ToString("0.00000"),
								 vSaldo.ToString("0.00000"),
								 vMedia.ToString("0.00000"),
								 (k-1).ToString()}));
								 /* -------------- */
		} // Fim do For;   
	} // Fim do IF;            
}//Fim do Método MontaMapaCapi;ra
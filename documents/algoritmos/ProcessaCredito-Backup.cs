        /* ---------------------------------------------- */
        #region:: 2 Métodos Sobrecarregados ProcessaCredito.


        /// <summary>
        ///  Método Sobrecarregado ProcessaCredito,
        ///  em caso de prêmio!
        /// </summary>
        /// <param name="gp_premiado"></param>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="qtdePremio"></param>
        /// <param name="lblQtdeSeriesPremiadas"></param>
        /// <param name="vCountCarDoPremio"></param>
        /// <param name="CtrlGpCredito"></param>
        /// <param name="aMapaCapi"></param>
        /// <param name="car"></param>
        /// <param name="vLucroIndividual"></param>
        public void ProcessaCredito(bool OnOff, int gp_premiado, int id, string status, ref int qtdePremio, 
                                    ref Label lblQtdeSeriesPremiadas, ref int[] vCountCarDoPremio, ref decimal[] CtrlGpCredito,
                                    ref string[,] aMapaCapi, int car, ref decimal vLucroIndividual)
        {
            if (car <= 15)
            {
                ZeraCarencia(id_serie);
                AumentaQtdePremio(id_serie);
                CtrlGpCredito[gp_premiado - 1] += Convert.ToDecimal(aMapaCapi[car, 5]);
                qtdePremio++;
                lblQtdeSeriesPremiadas.Text = qtdePremio.ToString();


                // Novo - Observar comportamento
                vLucroIndividual += Convert.ToDecimal(aMapaCapi[car, 6]);


                #region:: Seta o Array vCountCarAtual de acordo com a carência por Sb
                if (car < 15)
                {
                    vCountCarDoPremio[car]++;
                }
                #endregion

            } else {

                switch (status)
                {
                    case "Processing_115":
                        ZeraCarencia(id_serie);
                        ZeraCarenciaRecap(id_serie);
                        AumentaQtdePremio(id_serie);
                        CtrlGpCredito[gp_premiado - 1] += Convert.ToDecimal(aMapaCapi[car, 5]);
                        qtdePremio++;
                        lblQtdeSeriesPremiadas.Text = qtdePremio.ToString();

                        // Novo - Observar comportamento
                        vLucroIndividual += Convert.ToDecimal(aMapaCapi[car, 6]);


                        #region:: Seta o Array vCountCarAtual de acordo com a carência por Sb
                        if (car < 15)
                        {
                            vCountCarDoPremio[car]++;
                        }
                        #endregion
                        break;
                    case "Recap_wait_01":
                        IniciaNewStatus("Recap_Proc_01", id_serie, this.cn);
                        break;
                    case "Recap_wait_02":
                        IniciaNewStatus("Recap_Proc_02", id_serie, this.cn);
                        break;
                    case "Recap_wait_03":
                        IniciaNewStatus("Recap_Proc_03", id_serie, this.cn);
                        break;
                    case "Recap_wait_04":
                        IniciaNewStatus("Recap_Proc_04", id_serie, this.cn);
                        break;
                    case "Recap_wait_05":
                        IniciaNewStatus("Recap_Proc_05", id_serie, this.cn);
                        break;
                    case "Recap_Proc_01":
                        MudaStatus("Processing_115", id_serie, this.cn);
                        ZeraCarencia(id_serie);
                        ZeraCarenciaRecap(id_serie);
                        AumentaQtdePremio(id_serie);
                        CtrlGpCredito[gp_premiado - 1] += Convert.ToDecimal(aMapaCapi[car, 5]);
                        qtdePremio++;
                        lblQtdeSeriesPremiadas.Text = qtdePremio.ToString();

                        // Novo - Observar comportamento
                        vLucroIndividual += Convert.ToDecimal(aMapaCapi[car, 6]);


                        #region:: Seta o Array vCountCarAtual de acordo com a carência por Sb
                        if (car < 15)
                        {
                            vCountCarDoPremio[car]++;
                        }
                        #endregion

                        break;
                    case "Recap_Proc_02":
                        MudaStatus("Processing_115", id_serie, this.cn);
                        ZeraCarencia(id_serie);
                        ZeraCarenciaRecap(id_serie);
                        AumentaQtdePremio(id_serie);
                        CtrlGpCredito[gp_premiado - 1] += Convert.ToDecimal(aMapaCapi[car, 5]);
                        qtdePremio++;
                        lblQtdeSeriesPremiadas.Text = qtdePremio.ToString();

                        // Novo - Observar comportamento
                        vLucroIndividual += Convert.ToDecimal(aMapaCapi[car, 6]);


                        #region:: Seta o Array vCountCarAtual de acordo com a carência por Sb
                        if (car < 15)
                        {
                            vCountCarDoPremio[car]++;
                        }
                        #endregion
                        break;
                    case "Recap_Proc_03":
                        MudaStatus("Processing_115", id_serie, this.cn);
                        ZeraCarencia(id_serie);
                        ZeraCarenciaRecap(id_serie);
                        AumentaQtdePremio(id_serie);
                        CtrlGpCredito[gp_premiado - 1] += Convert.ToDecimal(aMapaCapi[car, 5]);
                        qtdePremio++;
                        lblQtdeSeriesPremiadas.Text = qtdePremio.ToString();

                        // Novo - Observar comportamento
                        vLucroIndividual += Convert.ToDecimal(aMapaCapi[car, 6]);


                        #region:: Seta o Array vCountCarAtual de acordo com a carência por Sb
                        if (car < 15)
                        {
                            vCountCarDoPremio[car]++;
                        }
                        #endregion
                        break;
                    case "Recap_Proc_04":
                        MudaStatus("Processing_115", id_serie, this.cn);
                        ZeraCarencia(id_serie);
                        ZeraCarenciaRecap(id_serie);
                        AumentaQtdePremio(id_serie);
                        CtrlGpCredito[gp_premiado - 1] += Convert.ToDecimal(aMapaCapi[car, 5]);
                        qtdePremio++;
                        lblQtdeSeriesPremiadas.Text = qtdePremio.ToString();

                        // Novo - Observar comportamento
                        vLucroIndividual += Convert.ToDecimal(aMapaCapi[car, 6]);


                        #region:: Seta o Array vCountCarAtual de acordo com a carência por Sb
                        if (car < 15)
                        {
                            vCountCarDoPremio[car]++;
                        }
                        #endregion
                        break;
                    case "Recap_Proc_05":
                        MudaStatus("Processing_115", id_serie, this.cn);
                        ZeraCarencia(id_serie);
                        ZeraCarenciaRecap(id_serie);
                        AumentaQtdePremio(id_serie);
                        CtrlGpCredito[gp_premiado - 1] += Convert.ToDecimal(aMapaCapi[car, 5]);
                        qtdePremio++;
                        lblQtdeSeriesPremiadas.Text = qtdePremio.ToString();

                        // Novo - Observar comportamento
                        vLucroIndividual += Convert.ToDecimal(aMapaCapi[car, 6]);


                        #region:: Seta o Array vCountCarAtual de acordo com a carência por Sb
                        if (car < 15)
                        {
                            vCountCarDoPremio[car]++;
                        }
                        #endregion
                        break;
                    //case "Subsidio_wait":
                    //    break;
                    //case "Subsidio_Ready":
                    //    break;
                    //default:
                    //    break;
                }// Fim do Switch / Case
                // ------------------ //
            }// Fim do If            
            // ------- //
        }// Fim do Método

        /// <summary>
        ///  Método Sobrecarregado ProcessaCredito,
        ///  em caso de NEGATIVA de prêmio!
        /// </summary>
        /// <param name="seriePremiada"></param>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="aMapaCapi"></param>
        /// <param name="car"></param>
        public void ProcessaCredito(bool OnOff, int id, string status, int car)
        {
            if (car < 15) {

                AumentaCarencia(id_serie);
                ComparaCarencia(id_serie);

            } else if (car == 15) {

                IniciaNewStatus("Recap_wait_01", id_serie, this.cn);
                AumentaCarencia(id_serie);
                ComparaCarencia(id_serie);

            } else if (car > 15) {

                switch (status)
                {
                    case "Processing_115":
                        // OBS.: O Status Processing_115 
                        // para Carecia = 16 não existe,
                        // pois, já no SB anterior este status
                        // foi alterado para Recap_wait_01
                        // AVALIAR COM MAIS DETALHES
                        if (car == 16) { CSharpUtil.Util.Msg("Este Status está errado, \n\rpois deveria ser Recap_wait_01"); }
                        break;
                        // De Recap_wait_01 até Recap_wait_05
                        // o procedimento é o mesmo e comum à
                        // qualquer opção para NÃO credito, e
                        // já está no final do Switch / Case!
                        /* ------------------------------- */
                    //case "Recap_wait_01":
                    //    break;
                    //case "Recap_wait_02":
                    //    break;
                    //case "Recap_wait_03":
                    //    break;
                    //case "Recap_wait_04":
                    //    break;
                    //case "Recap_wait_05":
                    //    break;
                        /* ------------------------------- */
                    case "Recap_Proc_01":
                        if (carencia_recap > 5)
                        {
                            IniciaNewStatus("Recap_wait_02", id_serie, this.cn);
                        } else {
                            AumentaCarenciaRecap(id_serie);
                            AumentaCarencia(id_serie);
                            ComparaCarencia(id_serie);
                        }
                        break;
                    case "Recap_Proc_02":
                        if (carencia_recap > 5)
                        {
                            IniciaNewStatus("Recap_wait_03", id_serie, this.cn);
                        } else {
                            AumentaCarenciaRecap(id_serie);
                            AumentaCarencia(id_serie);
                            ComparaCarencia(id_serie);
                        }
                        break;
                    case "Recap_Proc_03":
                        if (carencia_recap > 5) {
                            IniciaNewStatus("Recap_wait_04", id_serie, this.cn);
                        } else {
                            AumentaCarenciaRecap(id_serie);
                            AumentaCarencia(id_serie);
                            ComparaCarencia(id_serie);
                        }
                        break;
                    case "Recap_Proc_04":
                        if (carencia_recap > 5) {
                            IniciaNewStatus("Recap_wait_05", id_serie, this.cn);
                        } else {
                            AumentaCarenciaRecap(id_serie);
                            AumentaCarencia(id_serie);
                            ComparaCarencia(id_serie);
                        }
                        break;
                    case "Recap_Proc_05":
                        if (carencia_recap > 5) {
                            MudaStatus("Subsidio_wait", id_serie, this.cn);
                        } else {
                            AumentaCarenciaRecap(id_serie);
                            AumentaCarencia(id_serie);
                            ComparaCarencia(id_serie);
                        }
                        break;
                    //case "Subsidio_wait":
                    //    break;
                    //case "Subsidio_Ready":
                    //    break;
                    //default:
                    //    break;
                }// Fim do Switch

            }// Fim do else if (car == 15)

        }// Fim do Método ProcessaCredito (false)

        #endregion:: FIM: 2 Métodos Sobrecarregados ProcessaCredito.
        /* ------------------------------------------------------ */
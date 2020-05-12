using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace TesteAutomationPratice
{
    public class ComprarItem
    {

        [Theory]
        [InlineData(
            "Faded Short Sleeve T-shirts",
            "$18.51",
            "joaosouza32@teste.com",
            "id_gender1",
            "joao",
            "souza",
            "123456789",
            "24",
            "8",
            "1998",
            "rua tal",
            "curitiba",
            "6",
            "00000",
            "41995511835"
            )]
        public void ComprarRoupa(
            string tituloRoupa,
            string valorTotal,
            string emailCadastro,
            string nomeRadioSexo,
            string primeiroNome,
            string segundoNome,
            string senha,
            string dia,
            string mes,
            string ano,
            string endereco,
            string cidade,
            string estado,
            string cep,
            string celular
            )
        {
            /*Abre o Google Chrome e carrega a pagina*/
            IWebDriver _driver = new ChromeDriver(@"C:\\Users\\gui_j\\Desktop\\github\\Testes\\chormedriver");
            _driver.Navigate().GoToUrl("http://automationpractice.com/index.php");

            /*Cria o Timeout de 10 s*/
            var timeout = 10000; // in milliseconds
            var wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(timeout));

            /*Clica na roupa escolhida*/
            _driver.FindElement(By.LinkText(tituloRoupa)).Click();

            /*Espera a pagina carregar para continuar*/
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            /*Adiciona a roupa ai carrinho*/
            _driver.FindElement(By.Name("Submit")).Submit();

            /*Espera o modal aparecer para continuar*/
            wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Proceed to checkout")));

            /*Clica no botao para ir para o carrinho*/
            _driver.FindElement(By.LinkText("Proceed to checkout")).Click();

            var resultadoPrecoTotal = _driver.FindElement(By.Id("total_price")).Text;

            Assert.Equal(valorTotal, resultadoPrecoTotal);

            /*Clica no botao para se registar*/
            _driver.FindElement(By.LinkText("Proceed to checkout")).Click();

            /*Espera a pagina carregar para continuar*/
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            /*Coloca o email para cadastro*/
            _driver.FindElement(By.Name("email_create")).SendKeys(emailCadastro);

            /*Cria a conta com o email*/
            _driver.FindElement(By.Name("SubmitCreate")).Submit();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("submitAccount")));

            /*Coloca os dados para o cadastro*/

            _driver.FindElement(By.Id(nomeRadioSexo)).Click();

            _driver.FindElement(By.Name("customer_firstname")).SendKeys(primeiroNome);
            _driver.FindElement(By.Name("customer_lastname")).SendKeys(segundoNome);
            _driver.FindElement(By.Name("passwd")).SendKeys(senha);

            var optionDia = _driver.FindElement(By.Id("days"));
            var selectElementDia = new SelectElement(optionDia);
            selectElementDia.SelectByValue(dia);

            var optionMes = _driver.FindElement(By.Id("months"));
            var selectElementMes = new SelectElement(optionMes);
            selectElementMes.SelectByValue(mes);

            var optionAno = _driver.FindElement(By.Id("years"));
            var selectElementAno = new SelectElement(optionAno);
            selectElementAno.SelectByValue(ano);

            _driver.FindElement(By.Name("address1")).SendKeys(endereco);
            _driver.FindElement(By.Name("city")).SendKeys(cidade);

            var optionEstado = _driver.FindElement(By.Id("id_state"));
            var selectElementEstado = new SelectElement(optionEstado);
            selectElementEstado.SelectByValue(estado);

            _driver.FindElement(By.Name("postcode")).SendKeys(cep);

            var optionPais = _driver.FindElement(By.Id("id_country"));
            var selectElementPais = new SelectElement(optionPais);
            selectElementPais.SelectByValue("21");

            _driver.FindElement(By.Name("phone_mobile")).SendKeys(celular);


            /*Envia o formulario*/
            _driver.FindElement(By.Name("submitAccount")).Submit();

            /*Espera a pagina carregar para continuar*/
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            /*Envia o formulario*/
            _driver.FindElement(By.Name("processAddress")).Submit();

            /*Espera a pagina carregar para continuar*/
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            /*Aceita os termos da pagina*/
            _driver.FindElement(By.Id("cgv")).Click();

            /*Envia o formulario*/
            _driver.FindElement(By.Name("processCarrier")).Submit();

            /*Espera a pagina carregar para continuar*/
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));


            /*Verifica o valor total*/
            var resultadoFinal = _driver.FindElement(By.Id("total_price")).Text;

            Assert.Equal(valorTotal, resultadoFinal);

            /*Escolhe o metodo de pagamento*/
            _driver.FindElement(By.ClassName("bankwire")).Click();

            /*Espera a pagina carregar para continuar*/
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

            /*Clica no botao para confirmar a compra*/
            _driver.FindElement(By.CssSelector("button")).Submit();

            /*Volta para a Home*/
            _driver.Navigate().GoToUrl("http://automationpractice.com/index.php");

            /*Fecha o Google Chrome */
            //_driver.Quit();
            //_driver = null;

        }

    }
}
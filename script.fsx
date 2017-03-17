#r @"packages\Selenium.WebDriver\lib\net40\WebDriver.dll"
#r @"packages\canopy\lib\canopy.dll"
open canopy
open canopy.core
open System
open OpenQA.Selenium

phantomJSDir <- @"d:\Portable\phantomjs\bin\"

let exists selector =
    let someEle = someElement selector
    match someEle with
    | Some(_) -> true
    | None -> false

let shot () =
    let path = __SOURCE_DIRECTORY__ + @"\debug\"
    let filename = DateTime.Now.ToString("MMM-d_HH-mm-ss-fff")
    screenshot path filename

let (login, password) = "login", "pasword"

// Definitions
let btnCards = ".ui-sidebar__group-title-edit_cards"

start phantomJS
resize (1200, 900)
url "https://www.tinkoff.ru/"
shot()
browser.FindElement(By.XPath("//button")).Click();
shot()
if exists "[name=login]" then "[name=login]" << login
shot()
"[name=password]" << password
shot()
click ".ui-auth__form-submit"
shot()
waitForElement btnCards
shot()
let amount = List.head (fastTextFromCSS ".ui-money__amount.ui-sidebar__money-amount")

printfn "%s" amount

import { login } from "../modules/api.js";

document.getElementById("login_form").addEventListener("submit", async ev => {
    ev.preventDefault();
    let username = ev.target["username"].value;
    let password = ev.target["password"].value;
    let [ok, response] = await login(username, password);
    if (!ok) {
        alert(response.detail)
        return
    }
    localStorage.setItem("authorization", response.bearer_token);
    window.location.href = "../../home.html";
});
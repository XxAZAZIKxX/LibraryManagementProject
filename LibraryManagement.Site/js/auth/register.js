import { register } from "../modules/api.js";

const usernameRegExp = /^(?:(?:[A-z]|[0-9])(?!\s))+$/;

document.getElementById("register_form").addEventListener("submit", async (ev) => {
    ev.preventDefault();
    let username = ev.target["username"].value;
    let password = ev.target["password"].value;

    if (!usernameRegExp.test(username)) {
        alert("Username is invalid!")
        return;
    }

    let [ok, response] = await register(username, password)
    if (!ok) {
        alert(response.detail)
    } else {
        localStorage.setItem("authorization", response.bearer_token);
        window.location.href = "../../home.html"
    }
});
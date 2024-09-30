import { refresh } from "./modules/api.js";

let token = localStorage.getItem("authorization")
if (!token) window.location.href = "index.html";
let [ok, response] = await refresh()
if (!ok) {
    localStorage.removeItem("authorization");
    window.location.href = "index.html"
} else {
    localStorage.setItem("authorization", response.bearer_token);
}
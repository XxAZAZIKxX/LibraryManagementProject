import { addAuthor, getAuthor, updateAuthor, } from "../modules/api.js";

const urlParams = new URLSearchParams(window.location.search);
const authorId = urlParams.get("authorId");

(async function () {
    if (!authorId) return;
    try {
        let [ok, response] = await getAuthor(authorId)
        if (!ok) throw new Error(JSON.stringify(response));

        document.getElementById("form_title").innerText = `Author ${response.name} ${response.surname}`;
        document.getElementById("name").value = response.name;
        document.getElementById("surname").value = response.surname;
    }
    catch {
        window.location.href = "../../authors/authors.html"
    }
}());

document.getElementById("author_form").addEventListener("submit", async ev => {
    ev.preventDefault();
    let name = ev.target["name"].value;
    let surname = ev.target["surname"].value;

    if (!authorId) await addAuthor({
        name,
        surname
    })
    else await updateAuthor(authorId, name, surname)

    window.location.href = "../../authors/authors.html"
});
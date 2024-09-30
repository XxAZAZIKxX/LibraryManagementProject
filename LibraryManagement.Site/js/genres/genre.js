import { addGenre, getGenre, updateGenre } from "../modules/api.js";

const urlParams = new URLSearchParams(window.location.search);
const genreId = urlParams.get("genreId");

(async function () {
    if (!genreId) return;
    try {
        let [ok, response] = await getGenre(genreId)
        if (!ok) throw new Error(JSON.stringify(response));

        document.getElementById("form_title").innerText = `Genre "${response.title}"`;
        document.getElementById("title").value = response.title;
    }
    catch {
        window.location.href = "../../genres/genres.html"
    }
}());

document.getElementById("genre_form").addEventListener("submit", async ev => {
    ev.preventDefault();
    let title = ev.target["title"].value;

    if (!genreId) await addGenre({
        title
    })
    else await updateGenre(genreId, title)

    window.location.href = "../../genres/genres.html"
});
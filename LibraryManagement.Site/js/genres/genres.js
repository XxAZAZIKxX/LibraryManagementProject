import { deleteGenre, getGenres, refresh } from "../modules/api.js";

function stringFormat(str, ...args) {
    return str.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] != 'undefined' ? args[number] : match;
    });
}

const rowTemplate = `
<tr id="{0}">
    <td>{0}</td>
    <td>{1}</td>
    <td><a href="genre.html?genreId={0}" class="btn btn-outline-primary btn-sm" style="width: 65px;">Edit</button></td>
    <td><button genreId="{0}" class="btn btn-outline-danger btn-sm" style="width: 65px;">Delete</button></td>
</tr>
`

const onDeleteButtonClick = async (ev) => {
    let bookId = ev.target.getAttribute("genreId")
    await deleteGenre(bookId)
    document.location.reload()
}

const tryRefreshToken = async () => {
    let [ok, _] = await refresh()
    if (!ok) return false;
    return true;
};

try {
    let [ok, genres] = await getGenres();
    if (!ok) {
        let res = await tryRefreshToken()
        if (!res) document.location.href = "../../home.html";
        else document.location.reload()
    }
    genres.forEach(genre => {
        document.getElementById("items").insertAdjacentHTML('beforeend', stringFormat(rowTemplate,
            genre.id,
            genre.title
        ))
        let buttons = document.getElementById(genre.id).getElementsByTagName("button");
        buttons[0].addEventListener("click", onDeleteButtonClick);
    });
}
catch (e) {
    document.location.href = "../../home.html"
}
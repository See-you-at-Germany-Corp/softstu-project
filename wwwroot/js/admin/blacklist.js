function returnItem(blacklistItemID = 0) {
    const result = confirm("ยืนยันการคืนของ");

    if (result) { 
        const tr = removeCell("device-blacklist-table", blacklistItemID);
        insertToHistoryTable("blacklist-device-history-table", tr);
    }
}

function removeCell(tablename = "", itemID = 0) {
    const table = document.querySelector(`.${tablename}`);
    const tbody = table.getElementsByTagName("tbody")[0];
    const tr = tbody.querySelector(`#blacklist-${itemID}`);

    // console.log(`tbody`, tbody);
    // console.log(`tr`, tr);

    tbody.removeChild(tr);

    return tr;
}

function insertToHistoryTable(
    tablename = "",
    tr = document.createElement("tr")
) {
    const table = document.querySelector(`#${tablename}`);
    const tbody = table.getElementsByTagName("tbody")[0];
    
    tr.getElementsByTagName("td")[3].innerHTML = "คืน";

    tbody.prepend(tr);
    // table.prepend(tr);
}

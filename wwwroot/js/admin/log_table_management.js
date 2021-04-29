const filters = {
    created: 0,
    book_date: 1,
};

const IOnDateParams = {
    inputdateID: "",
    tableID: "",
    tbodyID: "",
    filterWith: "created",
    originTrLists: "",
    isFilter: false,
};

function onDateLoad(inputdateID) { 
    const inputdate = document.querySelector(`#${inputdateID}`); 
    inputdate.valueAsDate = new Date();
}

function onDateChange(onDateParams = IOnDateParams) {
    const { inputdateID, tableID, tbodyID, filterWith, originTrLists, isFilter } = {
        ...onDateParams,
    };

    const inputdate = document.querySelector(`#${inputdateID}`);
    const inputDate = new Date(inputdate.value).toDateString();
    const table = document.querySelector(`#${tableID}`);
    const tbody = table.querySelector(`#${tbodyID}`);
    const filterID = filters[filterWith];

    // console.log('inputdate :>> ', inputdate.value);
    // console.log('tbody :>> ', tbody);
    // console.log('trCollection :>> ', trCollection);

    const filteredTrLists = originTrLists.filter((tr) => {
        const tdLists = tr.getElementsByTagName("td");
        const tdDate = new Date(tdLists[filterID].innerHTML).toDateString();

        if (!isFilter) return inputDate === tdDate;
        else return true;
    });

    // console.log("filteredTrLists :>> ", filteredTrLists);

    table.removeChild(tbody);

    const newTbody = document.createElement("tbody");
    newTbody.id = `${tbodyID}`;

    filteredTrLists.map((filtered) => {
        newTbody.appendChild(filtered);
    });

    table.append(newTbody);
}

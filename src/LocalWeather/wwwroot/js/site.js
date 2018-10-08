// Write your JavaScript code.
loadAllEvents();

function loadAllEvents() {
    inputTextKeyDownEvent();
}

/* Event Listeners */
function inputTextKeyDownEvent() {
    let input = document.querySelector('#text-input');
    input.addEventListener('keydown', function () {
        if (event.keyCode == 13) {
            GetLocation(input);
        }
    })
};

/* Event Handlers */

async function GetLocation(input) {
        let respond = await fetch("/Location/GetLocation?textBoxSearch=" + input.value, {
            method: "post"
        });
        let result = await respond.json();
        if (result != 0) {
            clearContainer();
            createLocationContainer(result);
        }
}

async function getForecast(lat, lon) {
    let respond = await fetch("/Weather/GetForecast?lat=" + lat + "&lon=" + lon);
    let result = await respond.json();
    if (result != 0) {
        clearContainer();
        createForecastContainer(result);
    }
    };

/* Create functions */

function createSearchContainer() {
    let outerContainer = document.querySelector('#right-side');
    let container = createContainerElement(search);
    let header = createHeaderOneElement();
    let input = createInputElement();
    let footer = createFooterElement();
    container.appendChild(header);
    container.appendChild(input);
    container.appendChild(footer);
    outerContainer.appendChild(container);
}

function createLocationContainer(locations) {
    let outerContainer = document.querySelector('#right-side');
    let container = createContainerElement("location");
    let header = createHeaderTwoElement();
    container.appendChild(header);
    locations.forEach(function (location) {
        let label = createLocationLabelElement(location);
        container.appendChild(label);
    })
    outerContainer.appendChild(container);
}

function createForecastContainer(result) {
    let indexes = findForecastIndexes(result);
    let outerContainer = document.querySelector('#right-side');
    let container = createContainerElement("forecast");
    indexes.forEach(function (index) {
        let label = createForecastLabel(result.forecast[index]);
        container.appendChild(label);
    });
    outerContainer.appendChild(container);
};

function createForecastLabel(forecast) {
    let label = document.createElement('label');
    label.textContent = "Temperature: " + forecast.temperature + "Celsius";
    label.style.border = "0.01em solid black";
    label.style.backgroundColor = "white";
    label.style.padding = "1em";
    label.addEventListener("click", function () {
        createDetailedForecast(forecast);
    });
    return label;
}

function createDetailedForecastContainer(forecast) {
    let indexes = findDetailedForecastIndexes(forecast);
    let outerContainer = document.querySelector('#left-side');
    let container = createContainerElement("forecast");
    indexes.forEach(function (index) {
        let label = createForecastLabel(result.forecast[index]);
        container.appendChild(label);
    });
    outerContainer.appendChild(container);
};

function findDetailedForecastIndexes(forecast) {
    let indexes = [];
    result.forecast.forEach(function (time) {
        let hour = new Date(time.validTime).getHours();
        if (hour == 14) {
            indexes.push(result.forecast.indexOf(time));
        }
    });
    if (indexes.length < 10) {
        indexes.push(result[indexes.length - 1]);
    }
    return indexes; /* <----- you are here! */
};


function createContainerElement(id) {
    let element = document.createElement('div');
    element.id = id + "-container";
    return element;
}

function createHeaderOneElement() {
    let header = document.createElement('h1');
    header.id = "name-header";
    header.textContent = "Local Weather";
    return header;
}

function createHeaderTwoElement() {
    let header = document.createElement('h2');
    header.id = "name-header";
    header.textContent = "Locations";
    header.style.marginTop = "5%";
    return header;
}

function createInputElement() {
    let input = document.createElement('input');
    input.id = "text-input";
    input.type = "text";
    input.name = "textBoxSearch";
    return input;
}

function createFooterElement() {
    let footer = document.createElement('footer');
    let paragraph = document.createElement('p');
    paragraph.textContent = "Created by ";
    let anchor = document.createElement('a');
    anchor.textContent = "Martin Molin";
    anchor.href = "https://github.com/Mmmolin";
    paragraph.appendChild(anchor);
    footer.appendChild(paragraph);
    return footer;
}

function createLocationLabelElement(location) {
    let label = document.createElement('label');
    label.textContent = location.display_Name;
    label.style.flex = "1";
    label.style.height = "2em";
    label.style.backgroundColor = "white";
    label.style.border = "0.01em solid black";
    label.addEventListener('click', function () {
        getForecast(location.lat, location.lon);
    })
    return label;
}

/* Various functions */

function clearContainer() {
    document.querySelector('#right-side :first-child').remove();
}

/* This is awful, but it works. REFACTOR! regex? */
function findForecastIndexes(result) {
    let indexes = [];
    indexes.push(0);
    result.forecast.forEach(function (time) {
        let hour = new Date(time.validTime).getHours();
        if (hour == 14) {
            indexes.push(result.forecast.indexOf(time));
        }
    });
    if (indexes.length < 10) {
        indexes.push(result[indexes.length - 1]);
    }
    return indexes;
};



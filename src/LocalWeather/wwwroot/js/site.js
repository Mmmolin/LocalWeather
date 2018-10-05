// Write your JavaScript code.
loadAllEvents();

function loadAllEvents() {
    inputTextKeyDownEvent();
}

function inputTextKeyDownEvent() {
    let input = document.querySelector('#text-input');
    input.addEventListener('keydown', function () {
        if (event.keyCode == 13) {
            GetLocation(input);
        }
    })
};

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

function clearContainer() {
    document.querySelector('#right-side :first-child').remove();
}

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
    let container = createContainerElement("locations");
    let header = createHeaderTwoElement();
    container.appendChild(header);
    locations.forEach(function (location) {
        let label = createLabelElement(location);
        container.appendChild(label);
    })
    outerContainer.appendChild(container);
}

function createForecastContainer() {

}

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

function createLabelElement(location) {
    let label = document.createElement('label');
    label.textContent = location.display_Name;
    label.style.flex = "1";
    label.style.height = "2em";
    label.style.backgroundColor = "white";
    label.style.border = "0.01em solid black";
    label.addEventListener('click', function () {
        getWeather(location.lat, location.lon);
    })
    return label;
}

async function getWeather(lat, lon) {
    let respond = await fetch("/Weather/GetWeather?lat=" + lat + "&lon=" + lon);
    let result = await respond.json();
}
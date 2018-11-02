// Write your JavaScript code.
loadAllEvents();
loadBackground();

function loadAllEvents() {
    inputTextKeyDownEvent();
}

function loadBackground() {
    let backgrounds = ["skyguy.jpg", "skyfield.jpg"];
    let index = Math.floor(Math.random() * backgrounds.length);
    let backgroundLeft = document.querySelector('#left-side');
    backgroundLeft.style.backgroundImage = "url(/Images/background/" + backgrounds[index] + ")";
};

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
            clearRightContainer();
            createLocationContainer(result);
        }
}

async function getForecast(location) {
    let respond = await fetch("/Weather/GetForecast?lat=" + location.lat + "&lon=" + location.lon);
    let result = await respond.json();
    if (result != 0) {
        clearRightContainer();
        createForecastContainer(result, location);
    }
    };

/* Create functions */

function createSearchContainer() {
    let outerContainer = document.querySelector('#right-side');
    let container = createContainerElement("search-container");
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
    let container = createContainerElement("location-container");
    let header = createHeaderTwoElement("Locations");
    container.appendChild(header);
    locations.forEach(function (location) {
        let label = createLocationLabel(location);
        container.appendChild(label);
    })
    outerContainer.appendChild(container);
}

function createForecastContainer(result, location) {
    let indexes = findForecastIndexes(result);
    let outerContainer = document.querySelector('#right-side');
    let container = createContainerElement("forecast-container");
    let header = createHeaderTwoElement(location.display_Name);
    header.style.fontSize = "2em";
    header.style.marginBottom = "1em";
    outerContainer.appendChild(header);
    indexes.forEach(function (index) {
        let label = createForecastItem(result, index);
        container.appendChild(label);
    });
    loadBackground();
    outerContainer.appendChild(container);
};

function createForecastItem(result, index) {
    let date = dateBuilder(result.forecast[index].validTime);
    let forecastItem = document.createElement('div');
    forecastItem.className = "forecastItem";

    let dateLabel = document.createElement('label');
    if (date.hasOwnProperty('weekday')) {
        dateLabel.textContent = date.weekday + " ";
    }
    dateLabel.textContent += date.day;
    if (date.hasOwnProperty('month')) {
        dateLabel.TextContent += " " + date.month;
    }
    dateLabel.style.gridColumn = "1/3";
    dateLabel.style.textAlign = "center";
    dateLabel.style.paddingLeft = "0.2em";
    dateLabel.style.paddingRight = "0.2em";
    dateLabel.style.margin = "0em";

    let weatherSymbol = document.createElement('img');
    weatherSymbol.src = "/Images/weathericons/" + result.forecast[index].weatherCategory + ".png";
    weatherSymbol.style.width = "100%";

    let temperature = document.createElement('label');
    temperature.textContent = result.forecast[index].temperature + " °C";
    temperature.style.fontSize = "2em";

    let precipitation = document.createElement('label');
    precipitation.textContent = "Precipitation: " + result.forecast[index].precipitationMedian + "mm";
    precipitation.style.gridColumn = "1/3";
    precipitation.style.textAlign = "center";

    forecastItem.appendChild(dateLabel);
    forecastItem.appendChild(weatherSymbol);
    forecastItem.appendChild(temperature);
    forecastItem.appendChild(precipitation);
    forecastItem.addEventListener("click", function () {
        clearLeftContainer();
        createDetailedForecastContainer(result, index);
    });
    return forecastItem;
}

function dateBuilder(jsonDate) {
    const weekday = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
    const month = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec"];
    const nowDate = new Date();
    let nextDate = new Date();
    nextDate.setDate(nextDate.getDate() + 1);

    let convertedDate = new Date(jsonDate);
    let date = {
        day: convertedDate.getDate(),
        weekday: weekday[convertedDate.getDay()],
        month: month[convertedDate.getMonth()]
    };
    if (date.day == nowDate.getDate()) {
        date.day = "Today";
        delete date.weekday;
        delete date.month;
    }
    else if (date.day == nextDate.getDate()) {
        date.day = "Tomorrow";
        delete date.weekday;
        delete date.month;
    }
    return date;
}

function createDetailedForecastContainer(result, index) {
    let indexes = findDetailedForecastIndexes(result.forecast, index);
    let outerContainer = document.querySelector('#left-side');
    let container = createContainerElement("detailed-forecast-container");
    let detailedTopBar = createDetailedTopBar();
    outerContainer.appendChild(detailedTopBar);
    indexes.forEach(function (index) {
        let label = createDetailedForecastItem(result, index);
        container.appendChild(label);
    });
    outerContainer.appendChild(container);
};

//Refactor, we are doing this twice. Also in createForecastItem!
function createDetailedForecastItem(result, index) {
    let detailedForecastItem = document.createElement('div');
    detailedForecastItem.className = "detailedForecastItem";

    let timeData = document.createElement('label');
    timeData.textContent = new Date(result.forecast[index].validTime).getHours() + "h";
    detailedForecastItem.appendChild(timeData);

    let weatherSymbolData = document.createElement('img');
    weatherSymbolData.src = "/Images/weathericons/" + result.forecast[index].weatherCategory + ".png";
    weatherSymbolData.style.width = "100%";
    detailedForecastItem.appendChild(weatherSymbolData);

    let temperatureData = document.createElement('label');
    temperatureData.textContent = result.forecast[index].temperature + " °C";
    detailedForecastItem.appendChild(temperatureData);

    let precipitationData = document.createElement('label');
    precipitationData.textContent = result.forecast[index].precipitationMedian + "mm";
    detailedForecastItem.appendChild(precipitationData);

    let windData = document.createElement('label');
    windData.textContent = result.forecast[index].wind +
        "m/s  " + result.forecast[index].windDirection;
    detailedForecastItem.appendChild(windData);
    return detailedForecastItem;
}

function createDetailedTopBar() {
    let detailedTopBar = document.createElement('div');
    detailedTopBar.id = "detailedTopBar";
    let categories = ["Time", "Weather", "Temp", "Precipitation", "Wind"];
    categories.forEach(function (category) {
        let categoryLabel = document.createElement('label');
        categoryLabel.textContent = category;
        categoryLabel.style.lineHeight = "30px";
        detailedTopBar.appendChild(categoryLabel);
    });
    return detailedTopBar;
}

function findDetailedForecastIndexes(forecast, index) {
    let indexes = [];

    forecast.forEach(function (time) {
        let targetDate = new Date(forecast[index].validTime).getDate();
        let forecastDate = new Date(time.validTime).getDate();
        if (forecastDate == targetDate) {
            indexes.push(forecast.indexOf(time));
        }
    });
    return indexes;
};


function createContainerElement(id) {
    let element = document.createElement('div');
    element.id = id;
    return element;
}

function createHeaderOneElement() {
    let header = document.createElement('h1');
    header.id = "name-header";
    header.textContent = "Local Weather";
    return header;
}

function createHeaderTwoElement(text) {
    let header = document.createElement('h2');
    header.id = "name-header";
    header.textContent = text;
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

function createLocationLabel(location) {
    let label = document.createElement('label');
    label.textContent = location.display_Name;
    label.style.flex = "1";
    label.style.lineHeight = "3em";
    label.style.backgroundColor = "white";
    label.style.border = "0.01em solid black";
    label.style.textAlign = "center";
    label.addEventListener('click', function () {
        getForecast(location);
    })
    return label;
}

//function createLabel(weatherData) {
//    let label = document.createElement('label');
//    label.Text = weatherData;
//    return label;
//}

/* Various functions */

//Refactor this START
function clearRightContainer() {
    let container = document.querySelector('#right-side :first-child');
    if (container != null) {
        container.remove();
    };
}

function clearLeftContainer() {
    let detailedTopBar = document.querySelector('#detailedTopBar');
    let container = document.querySelector('#detailed-forecast-container');
    if (container != null) {
        detailedTopBar.remove();
        container.remove();
    };
}

// END, should be one function!

/* This is awful, but it works. REFACTOR! regex? */
function findForecastIndexes(result) {
    let indexes = [];
    indexes.push(0);
    result.forecast.forEach(function (time) {
        let nowDate = new Date();
        let forecastDate = new Date(time.validTime);
        if (forecastDate.getHours() == 13 && forecastDate.getDate() != nowDate.getDate()) {
            indexes.push(result.forecast.indexOf(time));
        }
    });
    if (indexes.length < 10) {
        indexes.push(result.forecast.length - 1);
    }
    return indexes;
};



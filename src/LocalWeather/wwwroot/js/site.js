// Write your JavaScript code.
loadAllEvents();

function loadAllEvents() {
    inputTextEnterEvent();
}

function inputTextKeyDownEvent() {
    let input = document.querySelector('#text-input');
    input.addEventListener('keydown', function() {
        if (event.keyCode == 13) {
            let respond = await fetch("/Location/GetLocation/textBoxSearch=" + input.value);
        }
    });
}



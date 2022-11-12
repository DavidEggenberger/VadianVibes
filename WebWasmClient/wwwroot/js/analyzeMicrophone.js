function analyzeMicrophone(dotnetReference, language, key) {
    let result;
    var speechConfig = SpeechSDK.SpeechConfig.fromSubscription(key, "switzerlandnorth");
    speechConfig.speechRecognitionLanguage = language;
    var audioConfig = SpeechSDK.AudioConfig.fromDefaultMicrophoneInput();
    recognizer = new SpeechSDK.SpeechRecognizer(speechConfig, audioConfig);

    recognizer.recognizeOnceAsync(
        function (result) {
            result += result.text;
            dotnetReference.invokeMethodAsync("MicrophoneAnalyzedCallback", result, language);

            recognizer.close();
            recognizer = undefined;
        },
        function (err) {
            phraseDiv.innerHTML += err;
            window.console.log(err);

            recognizer.close();
            recognizer = undefined;
        });

    window.onload += () => {
        document.getElementById("btnStop").addEventListener("click", () => {
            recognizer.close();
        });
    }
}
function startAudio() {
    document.getElementById('audio')?.play();
}
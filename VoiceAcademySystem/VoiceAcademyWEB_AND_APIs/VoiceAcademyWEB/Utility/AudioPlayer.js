// Variable global para almacenar el estado de reproducción
var audioPlayer = {
    audioContext: new AudioContext(),
    audioSource: null,
    isPlaying: false,
    currentTime: 0,
};

// Función para cargar y reproducir un archivo de audio
audioPlayer.play = function (audioUrl) {
    // Detener la reproducción actual, si la hay
    if (this.audioSource !== null) {
        this.audioSource.stop();
    }

    // Crear un nuevo nodo de fuente de audio
    this.audioSource = this.audioContext.createBufferSource();

    // Cargar el archivo de audio desde la URL proporcionada
    fetch(audioUrl)
        .then((response) => response.arrayBuffer())
        .then((buffer) => this.audioContext.decodeAudioData(buffer))
        .then((audioBuffer) => {
            this.audioSource.buffer = audioBuffer;
            this.audioSource.connect(this.audioContext.destination);
            this.audioSource.start(0, this.currentTime);
            this.isPlaying = true;
        })
        .catch((error) => {
            console.error('Error al cargar el archivo de audio:', error);
        });
};

// Función para pausar la reproducción de audio
audioPlayer.pause = function () {
    if (this.isPlaying && this.audioSource !== null) {
        this.audioSource.stop();
        this.isPlaying = false;
        this.currentTime += this.audioContext.currentTime;
    }
};

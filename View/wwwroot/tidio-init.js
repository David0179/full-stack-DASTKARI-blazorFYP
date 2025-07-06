
window.loadTidio = () => {
    if (document.getElementById('tidio-script')) return;

    const script = document.createElement('script');
    script.src = "https://code.tidio.co/9176fxmnnxjnoc0cmtbjweacwortbwcz.js";
    script.id = "tidio-script";
    script.async = true;
    document.body.appendChild(script);
};

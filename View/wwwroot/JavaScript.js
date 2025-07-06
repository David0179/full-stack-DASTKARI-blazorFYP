window.registerClickOutside = function (dotnetHelper) {
    document.addEventListener('click', function (e) {
        const menu = document.querySelector('.custom-mobile-menu');
        const toggler = document.querySelector('.navbar-toggler');

        if (menu && !menu.contains(e.target) && !toggler.contains(e.target)) {
            dotnetHelper.invokeMethodAsync('CloseMenu');
        }
    });
};

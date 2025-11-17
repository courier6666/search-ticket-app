// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const findMyTimeZone = () => {
    let timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;
    return timezone;
};

const cookieExists = () => {
    return document.cookie.split(';').some(c => c.trim().startsWith('UserContext'));
}

const setCookie = (name, value, expirationDays) => {
    const d = new Date();
    d.setTime(d.getTime() + (expirationDays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = name + "=" + encodeURIComponent(value) + ";" + expires + `;path=/; SameSite=Lax; Secure`;
}

const setUserContextCookieJson = () => {

    if (cookieExists)
        return;

    navigator.geolocation.getCurrentPosition((position) => {
       console.log(position);

        let cookieJson = {
            timeZone: timezone,
            location: {
                latitude: position.coords.latitude,
                longitude: position.coords.longitude,
            }
        }

        console.log(cookieJson);

        let jsonUserContextCookie = JSON.stringify(cookieJson);
        setCookie('UserContext', jsonUserContextCookie, 1);
        console.log(document.cookie);
    },
        () => {
            console.error("Failed to get location!");
        });

    let timezone = findMyTimeZone();

};

setUserContextCookieJson();
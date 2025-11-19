const findMyTimeZone = () => {
    let timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;
    return timezone;
};

const cookieExists = () => {
    return document.cookie.split(';').some(c => c.trim().startsWith("UserContextSearchAppCookie"));
}

const setCookie = (name, value, expirationDays) => {
    const d = new Date();
    d.setTime(d.getTime() + (expirationDays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = name + "=" + encodeURIComponent(value) + ";" + expires + `;path=/; SameSite=none; Secure`;
}

const setUserContextCookieJson = () => {

    if (cookieExists())
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
        setCookie('UserContextSearchAppCookie', jsonUserContextCookie, 1);
        console.log(document.cookie);
    },
        () => {
            console.error("Failed to get location!");
        });

    let timezone = findMyTimeZone();

};

console.log(document.cookie);

setUserContextCookieJson();
const SessionManager =
{
    getToken()
    {
        return sessionStorage.getItem("jwt");
    },

    setToken(token)
    {
        sessionStorage.setItem("jwt", token);
    },

    removeToken()
    {
        sessionStorage.removeItem("jwt");
    },
}

export default SessionManager;

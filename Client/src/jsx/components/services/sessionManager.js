const SessionManager =
{
    getToken()
    {
        return sessionStorage.getItem("jwt");
    },

    setSession(sessionInfo)
    {
        sessionStorage.setItem("authorized", true);
        sessionStorage.setItem("jwt", sessionInfo.token);
        sessionStorage.setItem("userId", sessionInfo.userId);
        sessionStorage.setItem("username", sessionInfo.username);
        sessionStorage.setItem("role", sessionInfo.role);
    },
    
    getSession()
    {
        return {
            token: sessionStorage.getItem('jwt'),
            userId: sessionStorage.getItem('userId'),
            username: sessionStorage.getItem('username'),
            role: sessionStorage.getItem('role'),
        };
    },

    removeSession()
    {
        sessionStorage.removeItem('jwt');
        sessionStorage.removeItem('userId');
        sessionStorage.removeItem('username');
        sessionStorage.removeItem('role');
        sessionStorage.removeItem("authorized");
    },

    isAuthorized()
    {
        return sessionStorage.getItem("authorized");
    },

    getRole()
    {
        return sessionStorage.getItem('role');
    }
}

export default SessionManager;

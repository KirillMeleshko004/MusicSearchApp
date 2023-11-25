import React from "react";

function ActiveRequestStatus({status, statusText, onClick})
{

    return (
        <div className={" bordered-block uppercase  unselectable red-border-on-hover " +
            " horizontal center-justified center-aligned full-height full-width normal " +
            " medium-spaced " + (status ? "accept accept-border " : "error error-border ")}
            onClick={onClick}>
            {statusText}
        </div>
    )
}

export default ActiveRequestStatus;
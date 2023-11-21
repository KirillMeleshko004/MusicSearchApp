import React from "react";

function RequestStatus({status, text})
{

    return (
        <div className={" bordered-block uppercase  unselectable " +
            " horizontal center-justified center-aligned full-height full-width normal " +
            " medium-spaced " + (status ? "accept accept-border " : "error error-border ")}>
            {text}
        </div>
    )
}

export default RequestStatus;
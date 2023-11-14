import React from "react";

function StatusBadge({status, posStatus, negStatus})
{

    return (
        <div id="status-badge">
            {status == true && (
                <div className="accept bordered-block uppercase accept-border unselectable 
                    horizontal center-justified center-aligned full-height full-width normal
                    medium-spaced">
                    {posStatus}
                </div>
            )}
            {status == false && (
                <div className="error bordered-block uppercase error-border unselectable 
                    horizontal center-justified center-aligned full-height full-width normal
                    medium-spaced">
                    {negStatus}
                </div>
            )}
        </div>
    )
}

export default StatusBadge;
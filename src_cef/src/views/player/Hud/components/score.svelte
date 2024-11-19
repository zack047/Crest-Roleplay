<script>
  import hudConfig from '../hud.config.js';

  const team = ( id ) => { return hudConfig.teams[id] };

  let show = false,
    time = "05:34",
    leftTeam = {
        id: "vagos",
        count: 32,
    },
    rightTeam ={
        id: "families",
        count: 32,
    };

</script>

{#if show}
<div class="hud__score">
    <div class="hud__score_team left">
        <div class="hud__score_team-text">
            <h1>{team(leftTeam.id).title}</h1>
            <h2 style="color: {team(leftTeam.id).colors.subtitle}">{team(leftTeam.id).subtitle}</h2>
        </div>
        <div class="hud__score_team-icon">
            <svg width="47" height="53" viewBox="0 0 47 53" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M25.75 1.87639L43.6997 12.2396C45.092 13.0435 45.9497 14.5291 45.9497 16.1368V36.8633C45.9497 38.4709 45.092 39.9565 43.6997 40.7604L25.75 51.1236C24.3577 51.9275 22.6423 51.9275 21.25 51.1236L3.30033 40.7604C1.90802 39.9565 1.05033 38.4709 1.05033 36.8632V16.1368C1.05033 14.5291 1.90802 13.0435 3.30033 12.2396L21.25 1.87639C22.6423 1.07254 24.3577 1.07254 25.75 1.87639Z" fill="url(#leftTeam)" :stroke="team(leftTeam.id).colors.stroke"/>
                <defs>
                    <radialGradient id="leftTeam" cx="0" cy="0" r="1" gradientUnits="userSpaceOnUse" gradientTransform="translate(23.5 26.5) rotate(53.2463) scale(49.3001)">
                        <stop :stop-color="team(leftTeam.id).colors.polygon[0]"/>
                        <stop offset="1" :stop-color="team(leftTeam.id).colors.polygon[1]" stop-opacity="0.65"/>
                    </radialGradient>
                </defs>
            </svg>
            <h1 style="color: {team(leftTeam.id).colors.number}">{leftTeam.count}</h1>
                
        </div>
    </div>

    <div class="hud__score_time">
        <h1>Время</h1>
        <h2>{time}</h2>
    </div>

    <div class="hud__score_team right">
        <div class="hud__score_team-icon">
            <svg width="47" height="53" viewBox="0 0 47 53" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M25.75 1.87639L43.6997 12.2396C45.092 13.0435 45.9497 14.5291 45.9497 16.1368V36.8633C45.9497 38.4709 45.092 39.9565 43.6997 40.7604L25.75 51.1236C24.3577 51.9275 22.6423 51.9275 21.25 51.1236L3.30033 40.7604C1.90802 39.9565 1.05033 38.4709 1.05033 36.8632V16.1368C1.05033 14.5291 1.90802 13.0435 3.30033 12.2396L21.25 1.87639C22.6423 1.07254 24.3577 1.07254 25.75 1.87639Z" fill="url(#rightTeam)" :stroke="team(rightTeam.id).colors.stroke"/>
                <defs>
                    <radialGradient id="rightTeam" cx="0" cy="0" r="1" gradientUnits="userSpaceOnUse" gradientTransform="translate(23.5 26.5) rotate(53.2463) scale(49.3001)">
                        <stop :stop-color="team(rightTeam.id).colors.polygon[0]"/>
                        <stop offset="1" :stop-color="team(rightTeam.id).colors.polygon[1]" stop-opacity="0.65"/>
                    </radialGradient>
                </defs>
            </svg>
            <h1 style="color: {team(rightTeam.id).colors.number}">{rightTeam.count}</h1>
        </div>
        <div class="hud__score_team-text">
            <h1>{team(rightTeam.id).title}</h1>
            <h2 style="color: {team(rightTeam.id).colors.subtitle}">{team(rightTeam.id).subtitle}</h2>
        </div>
    </div>
</div>
{/if}
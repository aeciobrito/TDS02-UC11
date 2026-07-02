// Altere a porta de acordo com o que o Visual Studio / .NET CLI mostrar no seu terminal
// Exemplo: http://localhost:5032
const API_BASE_URL = 'http://localhost:5143';
async function fetchWithToken(url, options = {}) {
    const token = localStorage.getItem('token');
    const headers = { ...options.headers };
    if (token) headers['Authorization'] = 'Bearer ' + token;
    const finalOptions = { ...options, headers };
    if (finalOptions.body && !(finalOptions.body instanceof FormData) && typeof finalOptions.body === 'object') {
        finalOptions.headers['Content-Type'] = 'application/json';
        finalOptions.body = JSON.stringify(finalOptions.body);
    }
    const response = await fetch(url, finalOptions);
    if (response.status === 401) {
        localStorage.removeItem('token');
        if(window.location.pathname.includes('/html/')) {
            window.location.href = '../../index.html';
        } else {
            window.location.href = 'index.html';
        }
    }
    return response;
}

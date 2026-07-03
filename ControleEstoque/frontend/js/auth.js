function checkAuthentication() {
    const token = localStorage.getItem('token');
    if (!token) {
        // Se estiver em uma subpasta /html, volta para o index.html na raiz
        const path = window.location.pathname.includes('/html/') ? '../../index.html' : 'index.html';
        window.location.href = path + '?unauthorized=true';
    }
}

checkAuthentication();

//NAVIGATION
let menuBtn = document.querySelector('.customNavMobileMenuBtn');
menuBtn.addEventListener('click', () => {

    menuBtn.classList.toggle('customNavMobileMenuBtnChangeShapeToClose');
    let header = document.querySelector('header');
    header.classList.toggle('toggleMobileMenu');
    header.style.display = "unset";
});



//FOOTER CAROUSELE
const techIconsBox = document.querySelector('.footerTechLogos');
const techIcons = document.querySelectorAll('.footerTechLogosBox img');

const generateIcons = () => {

    techIcons.forEach(item => {

        let sourceImage = document.createElement('img');
        sourceImage.src = item.src;
        techIconsBox.appendChild(sourceImage)

    });

};
generateIcons();

setInterval(generateIcons, 20000);


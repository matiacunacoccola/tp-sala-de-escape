let intentos=0;
let palabra = document.getElementById("palabra").value;
let palabraOculta=[];
let gano=false;

for(let i=0; i<palabra.length; i++)
{
    palabraOculta.push("_");
}

let texto="";

for(let i=0; i<palabraOculta.length;i++)
{
  texto+= palabraOculta[i] + " ";
}

document.getElementById("palabraOculta").innerHTML=texto;

function arriesgarLetra()
{
  if(gano==true)
  {
     return;
  }

  if(intentos >=10)
  {
     return;
  }

  let letra=document.getElementById("letra").value.toUpperCase();

  if(letra.length!=1)
  {
     document.getElementById("mensaje").innerHTML="Ingrese una sola letra";
     return;
  }

  let encontrada=false;

  for(let i=0; i<palabra.length;i++)
  {
    if(palabra[i]==letra){
        palabraOculta[i]=letra;
        encontrada=true;
     }
  }

  let texto = "";
  let quedanGuiones=false;

  for(let i=0; i<palabraOculta.length; i++)
  {
    texto += palabraOculta[i] + " ";

    if(palabraOculta[i] =="_"){
     quedanGuiones=true;
    }
  }
  document.getElementById("palabraOculta").innerHTML = texto;

  if(quedanGuiones==false){
     document.getElementById("mensaje").innerHTML="Ganaste";
     gano=true;
     document.getElementById("continuar").style.display="block";
     return;
  }
  if(encontrada==false)
 {
     intentos ++;
  }
  document.getElementById("intentos").innerHTML= 10- intentos;
  if(intentos >= 10)
 {
     document.getElementById("mensaje").innerHTML="Perdiste. La palabra era: " + palabra;
     return;
  }
}
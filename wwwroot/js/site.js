let palabra = document.getElementById("palabra").value;
let palabraOculta=[];

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
  let letra=document.getElementById("letra").value.toUpperCase();

  if(letra.length!=1)
  {
     document.getElementById("mensaje").innerHTML="Ingrese una sola letra";
     return;
  }

  for(let i=0; i<palabra.length;i++)
  {
    if(palabra[i]==letra){
        palabraOculta[i]=letra;
     }
  }

  let texto = "";
  let quedanGuiones=false;
  for(let i=0; i<palabraOculta.length; i++)
  {
    texto += palabraOculta[i] + " ";

    if(palabraOculta[i] =="_")
   {
     quedanGuiones=true;
    }
  }
  document.getElementById("palabraOculta").innerHTML = texto;

  if(quedanGuiones==false)
{
     window.location.href = "/Juego/ResolverAhorcado";
     return;
  }
}
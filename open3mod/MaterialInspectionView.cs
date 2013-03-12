///////////////////////////////////////////////////////////////////////////////////
// Open 3D Model Viewer (open3mod) (v0.1)
// [TextureInspectionView.cs]
// (c) 2012-2013, Alexander C. Gessler
//
// Licensed under the terms and conditions of the 3-clause BSD license. See
// the LICENSE file in the root folder of the repository for the details.
//
// HIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND 
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
///////////////////////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using Assimp;

namespace open3mod
{
    public class MaterialInspectionView : ThumbnailViewBase<MaterialThumbnailControl>
    {
        private readonly Scene _scene;
      
        private delegate void SetLabelTextDelegate(string name, Texture tex);

        public MaterialInspectionView(Scene scene, FlowLayoutPanel flow)
            : base(flow)
        {
            _scene = scene;
     
            foreach (var mat in scene.Raw.Materials)
            {
                var dependencies = new HashSet<string>();
                var textures = mat.GetAllTextures();
                foreach (var tex in textures)
                {      
                    dependencies.Add(tex.FilePath);                  
                }

                AddMaterialEntry(mat, dependencies);                
            }
            /*
            var countdown = have.Count;
            Scene.TextureSet.AddCallback((name, tex) =>
            {
                // we need to handle this case because texture callbacks may occur late
                if (Flow.IsDisposed)
                {
                    return false;
                }

                if (_flow.IsHandleCreated)
                {
                    _flow.BeginInvoke(new SetLabelTextDelegate(SetTextureToLoadedStatus),
                                      new object[] { name, tex }
                        );
                }
                else
                {
                    SetTextureToLoadedStatus(name, tex);
                }
                return --countdown > 0;
            }); */

        }

      

        public Scene Scene
        {
            get { return _scene; }
        }


        private void AddMaterialEntry(Material material, HashSet<string> dependencies)
        {
            AddEntry(new MaterialThumbnailControl(this, Scene, material));
        }
    }
}

/* vi: set shiftwidth=4 tabstop=4: */
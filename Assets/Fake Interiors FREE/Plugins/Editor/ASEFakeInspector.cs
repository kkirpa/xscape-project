// Fake Interiors FREE
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>

using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using AseFakeInterior;
#if UNITY_2017_4_OR_NEWER
using UnityEngine.Networking;
#endif
#if AMPLIFY_SHADER_EDITOR
using AmplifyShaderEditor;
#endif

namespace AseFakeInterior
{
	internal class AboutAffiliate : EditorWindow
	{
		public static void Init()
		{
			AboutAffiliate window = (AboutAffiliate)GetWindow( typeof( AboutAffiliate ), true, "Affiliate Link Information" );
			window.minSize = new Vector2( 400, 130 );
			window.maxSize = new Vector2( 400, 130 );
			window.Show();
		}

		public void OnGUI()
		{
			GUIStyle lableStyle = new GUIStyle( GUI.skin.label );
			lableStyle.wordWrap = true;
			lableStyle.alignment = TextAnchor.MiddleCenter;
			lableStyle.richText = true;

			GUIStyle textLink = new GUIStyle( GUI.skin.label );
			textLink.normal.textColor = EditorGUIUtility.isProSkin ? ASEFakeInspector.LinkColorInPro : ASEFakeInspector.LinkColorInPersonal;
			//textLink.normal.textColor = new Color( 0.3f, 0.5f, 1 );
			GUILayout.Space( 16 );
			GUILayout.Label( "By using our Publisher Affiliate link we receive a small commission for all Asset Store purchases done in the following 7 days. If you want to support free content such as the one you are using, please consider using our unique Publisher link. Thank You!", lableStyle );
			GUILayout.Space( 16 );
			GUILayout.BeginHorizontal();
			if( GUILayout.Button( "Learn more about the Unity Affiliate program <color=#4C7DFFFF>here</color>", lableStyle ) )
			{
				Help.BrowseURL( "https://unity3d.com/affiliates" );
			}
			GUILayout.EndHorizontal();
		}
	}

	[Serializable]
	internal class BannerInfo
	{
		public string imageURL;
		public string message;
		public string affiliateLink;

		public static BannerInfo CreateFromJSON( string jsonString )
		{
			return JsonUtility.FromJson<BannerInfo>( jsonString );
		}

		public BannerInfo( string i, string m, string a )
		{
			imageURL = i;
			message = m;
			affiliateLink = a;
		}
	}
}

internal class ASEFakeInspector : ShaderGUI
{
	private MaterialEditor m_materialEditor;
	private GUIStyle m_linkStyle;
	private GUIStyle m_lableStyle;
	private GUIStyle m_textLink;
	private GUIContent m_button = new GUIContent( "" );

	private bool m_initialized = false;
	private bool m_imageLoaded = false;

	private int m_maxheight = 140;
	private float m_currheight = 140;
	private float m_imageRatio = 0.31818f;

	public static Color LinkColorInPro = new Color( 0.3f, 0.5f, 1 );
	public static Color LinkColorInPersonal = new Color( 0.1f, 0.3f, 0.8f );
	private Texture2D m_fetchedImage = null;
	private Texture2D m_defaultImage;

	private string m_jsonURL = "http://amplify.pt/Banner/FakeInteriorInfo.json";
	private BannerInfo m_info = new BannerInfo(
		"http://amplify.pt/Banner/Banner_FakeInterior.jpg",
		"Made with Amplify Shader Editor. Fully editable, unleash its full power with the editor and adapt it to any project.",
		StartScreen.Urls[0] );

	IEnumerator coroutine;
#if UNITY_2017_4_OR_NEWER
	UnityWebRequest m_webRequest;
	IEnumerator StartRequest( string url )
	{
		m_webRequest = UnityWebRequest.Get( url );
		yield return m_webRequest.SendWebRequest();
	}

	IEnumerator StartImageRequest( string url )
	{
		m_webRequest = UnityWebRequestTexture.GetTexture( url );
		yield return m_webRequest.SendWebRequest();
	}

	public void EditorUpdateFetchInfo()
	{
		UnityWebRequestAsyncOperation www = (UnityWebRequestAsyncOperation)coroutine.Current;
		if( !coroutine.MoveNext() )
		{
			if( !www.isDone )
			{
				coroutine.MoveNext();
			}
			else
			{
#if UNITY_2020_1_OR_NEWER
				if( m_webRequest.result == UnityWebRequest.Result.Success )
#else
				if( !m_webRequest.isNetworkError && !m_webRequest.isHttpError )
#endif
				{
					m_info = BannerInfo.CreateFromJSON( m_webRequest.downloadHandler.text );
					m_materialEditor.Repaint();

					EditorApplication.update += EditorUpdateFetchImage;
					coroutine = StartImageRequest( m_info.imageURL );
				}
				EditorApplication.update -= EditorUpdateFetchInfo;
			}
		}
	}

	public void EditorUpdateFetchImage()
	{
		UnityWebRequestAsyncOperation www = (UnityWebRequestAsyncOperation)coroutine.Current;
		if( !coroutine.MoveNext() )
		{
			if( !www.isDone )
			{
				coroutine.MoveNext();
			}
			else
			{
#if UNITY_2020_1_OR_NEWER
				if( m_webRequest.result == UnityWebRequest.Result.Success )
#else
				if( !m_webRequest.isNetworkError && !m_webRequest.isHttpError )
#endif
				{
					Texture2D myTexture = ( (DownloadHandlerTexture)m_webRequest.downloadHandler ).texture;
					m_fetchedImage = new Texture2D( myTexture.width, myTexture.height, TextureFormat.RGB24, false, true );
					m_fetchedImage.SetPixels( myTexture.GetPixels() );
					m_fetchedImage.Apply();
					m_materialEditor.Repaint();
				}
				EditorApplication.update -= EditorUpdateFetchImage;
			}
		}
	}
#else
				IEnumerator StartRequest( string url )
	{
		WWW www = new WWW( url );
		yield return www;
	}

	public void EditorUpdateFetchInfo()
	{
		WWW www = (WWW)coroutine.Current;
		if( !coroutine.MoveNext() )
		{
			if( !www.isDone )
			{
				coroutine.MoveNext();
			}
			else
			{
				if( string.IsNullOrEmpty( www.error ) )
				{
					m_info = BannerInfo.CreateFromJSON( www.text );
					m_materialEditor.Repaint();

					EditorApplication.update += EditorUpdateFetchImage;
					coroutine = StartRequest( m_info.imageURL );
				}
				EditorApplication.update -= EditorUpdateFetchInfo;
			}
		}
	}

	public void EditorUpdateFetchImage()
	{
		WWW www = (WWW)coroutine.Current;
		if( !coroutine.MoveNext() )
		{
			if( !www.isDone )
			{
				coroutine.MoveNext();
			}
			else
			{
				if( string.IsNullOrEmpty( www.error ) )
				{
					m_fetchedImage = new Texture2D( www.texture.width, www.texture.height, TextureFormat.RGB24, false, true );
					m_fetchedImage.SetPixels( www.texture.GetPixels() );
					m_fetchedImage.Apply();
					m_materialEditor.Repaint();
				}
				EditorApplication.update -= EditorUpdateFetchImage;
			}
		}
	}
#endif
	public override void OnGUI( MaterialEditor materialEditor, MaterialProperty[] properties )
	{
		if( !m_initialized )
		{
			m_initialized = true;
			m_linkStyle = new GUIStyle( GUIStyle.none );
			m_linkStyle.alignment = TextAnchor.UpperCenter;

			m_lableStyle = new GUIStyle( GUI.skin.label );

			m_lableStyle.wordWrap = true;
			m_lableStyle.alignment = TextAnchor.MiddleLeft;
			m_defaultImage = AssetDatabase.LoadAssetAtPath<Texture2D>( AssetDatabase.GUIDToAssetPath( "19890ee0a82f7a643b9077407a6da3db" ) );
			m_imageRatio = (float)m_defaultImage.height / (float)m_defaultImage.width;
			m_maxheight = m_defaultImage.height;

			m_textLink = new GUIStyle( GUI.skin.label );
			m_textLink.normal.textColor = Color.white;
			m_textLink.alignment = TextAnchor.MiddleCenter;
			m_textLink.fontSize = 9;
		}

		m_materialEditor = materialEditor;
		Material material = materialEditor.target as Material;

		if( m_fetchedImage != null )
		{
			m_button.image = m_fetchedImage;
			m_imageRatio = (float)m_fetchedImage.height / (float)m_fetchedImage.width;
			m_maxheight = m_fetchedImage.height;
		}
		else
		{
			m_button.image = m_defaultImage;
			m_imageRatio = 0.31818f;
			m_maxheight = 140;

			if( !m_imageLoaded )
			{
				m_imageLoaded = true;

				EditorApplication.update += EditorUpdateFetchInfo;
				coroutine = StartRequest( m_jsonURL );
			}
		}

		EditorGUILayout.BeginVertical( "ObjectFieldThumb" );
		{
			m_currheight = Mathf.Min( m_maxheight, ( EditorGUIUtility.currentViewWidth - 30 ) * m_imageRatio );
			Rect buttonRect = EditorGUILayout.GetControlRect( false, m_currheight );
			EditorGUIUtility.AddCursorRect( buttonRect, MouseCursor.Link );
			if( GUI.Button( buttonRect, m_button, m_linkStyle ) )
			{
				Help.BrowseURL( m_info.affiliateLink );
			}
			GUILayout.BeginHorizontal();
			GUILayout.Label( m_info.message, m_lableStyle/*, GUILayout.MinHeight(40)*/ );
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			if( GUILayout.Button( "Learn More", GUILayout.Height( 25 ) ) )
			{
				Help.BrowseURL( m_info.affiliateLink );
			}
			Color cache = GUI.color;
			GUI.color = EditorGUIUtility.isProSkin ? LinkColorInPro : LinkColorInPersonal;
			if( GUILayout.Button( "Affiliate Link", m_textLink ) )
			{
				AboutAffiliate.Init();
			}
			GUI.color = cache;
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}
		EditorGUILayout.EndVertical();

#if AMPLIFY_SHADER_EDITOR
		if( Event.current.type == EventType.Repaint &&
			material.HasProperty( IOUtils.DefaultASEDirtyCheckId ) &&
			material.GetInt( IOUtils.DefaultASEDirtyCheckId ) == 1 )
		{
			material.SetInt( IOUtils.DefaultASEDirtyCheckId, 0 );
			UIUtils.ForceUpdateFromMaterial();
#if !UNITY_5_5_OR_NEWER
			Event.current.Use();
#endif
		}

		if( GUILayout.Button( "Open in Shader Editor" ) )
		{
			AmplifyShaderEditorWindow.LoadMaterialToASE( material );
		}

		GUILayout.BeginHorizontal();
		{
			if( GUILayout.Button( "Copy Values" ) )
			{
				Shader shader = material.shader;
				int propertyCount = UnityEditor.ShaderUtil.GetPropertyCount( shader );
				string allProperties = string.Empty;
				for( int i = 0; i < propertyCount; i++ )
				{
					UnityEditor.ShaderUtil.ShaderPropertyType type = UnityEditor.ShaderUtil.GetPropertyType( shader, i );
					string name = UnityEditor.ShaderUtil.GetPropertyName( shader, i );
					string valueStr = string.Empty;
					switch( type )
					{
						case UnityEditor.ShaderUtil.ShaderPropertyType.Color:
						{
							Color value = material.GetColor( name );
							valueStr = value.r.ToString() + IOUtils.VECTOR_SEPARATOR +
										value.g.ToString() + IOUtils.VECTOR_SEPARATOR +
										value.b.ToString() + IOUtils.VECTOR_SEPARATOR +
										value.a.ToString();
						}
						break;
						case UnityEditor.ShaderUtil.ShaderPropertyType.Vector:
						{
							Vector4 value = material.GetVector( name );
							valueStr = value.x.ToString() + IOUtils.VECTOR_SEPARATOR +
										value.y.ToString() + IOUtils.VECTOR_SEPARATOR +
										value.z.ToString() + IOUtils.VECTOR_SEPARATOR +
										value.w.ToString();
						}
						break;
						case UnityEditor.ShaderUtil.ShaderPropertyType.Float:
						{
							float value = material.GetFloat( name );
							valueStr = value.ToString();
						}
						break;
						case UnityEditor.ShaderUtil.ShaderPropertyType.Range:
						{
							float value = material.GetFloat( name );
							valueStr = value.ToString();
						}
						break;
						case UnityEditor.ShaderUtil.ShaderPropertyType.TexEnv:
						{
							Texture value = material.GetTexture( name );
							valueStr = AssetDatabase.GetAssetPath( value );
						}
						break;
					}

					allProperties += name + IOUtils.FIELD_SEPARATOR + type + IOUtils.FIELD_SEPARATOR + valueStr;

					if( i < ( propertyCount - 1 ) )
					{
						allProperties += IOUtils.LINE_TERMINATOR;
					}
				}
				EditorPrefs.SetString( IOUtils.MAT_CLIPBOARD_ID, allProperties );
			}

			if( GUILayout.Button( "Paste Values" ) )
			{
				string propertiesStr = EditorPrefs.GetString( IOUtils.MAT_CLIPBOARD_ID, string.Empty );
				if( !string.IsNullOrEmpty( propertiesStr ) )
				{
					string[] propertyArr = propertiesStr.Split( IOUtils.LINE_TERMINATOR );
					bool validData = true;
					try
					{
						for( int i = 0; i < propertyArr.Length; i++ )
						{
							string[] valuesArr = propertyArr[ i ].Split( IOUtils.FIELD_SEPARATOR );
							if( valuesArr.Length != 3 )
							{
								Debug.LogWarning( "Material clipboard data is corrupted" );
								validData = false;
								break;
							}
							else if( material.HasProperty( valuesArr[ 0 ] ) )
							{
								UnityEditor.ShaderUtil.ShaderPropertyType type = (UnityEditor.ShaderUtil.ShaderPropertyType)Enum.Parse( typeof( UnityEditor.ShaderUtil.ShaderPropertyType ), valuesArr[ 1 ] );
								switch( type )
								{
									case UnityEditor.ShaderUtil.ShaderPropertyType.Color:
									{
										string[] colorVals = valuesArr[ 2 ].Split( IOUtils.VECTOR_SEPARATOR );
										if( colorVals.Length != 4 )
										{
											Debug.LogWarning( "Material clipboard data is corrupted" );
											validData = false;
											break;
										}
										else
										{
											material.SetColor( valuesArr[ 0 ], new Color( Convert.ToSingle( colorVals[ 0 ] ),
																						Convert.ToSingle( colorVals[ 1 ] ),
																						Convert.ToSingle( colorVals[ 2 ] ),
																						Convert.ToSingle( colorVals[ 3 ] ) ) );
										}
									}
									break;
									case UnityEditor.ShaderUtil.ShaderPropertyType.Vector:
									{
										string[] vectorVals = valuesArr[ 2 ].Split( IOUtils.VECTOR_SEPARATOR );
										if( vectorVals.Length != 4 )
										{
											Debug.LogWarning( "Material clipboard data is corrupted" );
											validData = false;
											break;
										}
										else
										{
											material.SetVector( valuesArr[ 0 ], new Vector4( Convert.ToSingle( vectorVals[ 0 ] ),
																						Convert.ToSingle( vectorVals[ 1 ] ),
																						Convert.ToSingle( vectorVals[ 2 ] ),
																						Convert.ToSingle( vectorVals[ 3 ] ) ) );
										}
									}
									break;
									case UnityEditor.ShaderUtil.ShaderPropertyType.Float:
									{
										material.SetFloat( valuesArr[ 0 ], Convert.ToSingle( valuesArr[ 2 ] ) );
									}
									break;
									case UnityEditor.ShaderUtil.ShaderPropertyType.Range:
									{
										material.SetFloat( valuesArr[ 0 ], Convert.ToSingle( valuesArr[ 2 ] ) );
									}
									break;
									case UnityEditor.ShaderUtil.ShaderPropertyType.TexEnv:
									{
										material.SetTexture( valuesArr[ 0 ], AssetDatabase.LoadAssetAtPath<Texture>( valuesArr[ 2 ] ) );
									}
									break;
								}
							}
						}
					}
					catch( Exception e )
					{
						Debug.LogException( e );
						validData = false;
					}


					if( validData )
					{
						materialEditor.PropertiesChanged();
						UIUtils.CopyValuesFromMaterial( material );
					}
					else
					{
						EditorPrefs.SetString( IOUtils.MAT_CLIPBOARD_ID, string.Empty );
					}
				}
			}
		}
		GUILayout.EndHorizontal();
		GUILayout.Space( 5 );

#endif

#if AMPLIFY_SHADER_EDITOR
		EditorGUI.BeginChangeCheck();
		ShaderPropertiesGUI( material, properties );
		if( EditorGUI.EndChangeCheck() )
		{
			UIUtils.CopyValuesFromMaterial( material );
		}
#else
		ShaderPropertiesGUI( material, properties );
#endif
	}

	public void ShaderPropertiesGUI( Material material, MaterialProperty[] properties )
	{
		m_materialEditor.SetDefaultGUIWidths();
		EditorGUIUtility.labelWidth = 0f;

		for( int i = 0; i < properties.Length; i++ )
		{
			if( ( properties[ i ].flags & ( MaterialProperty.PropFlags.HideInInspector | MaterialProperty.PropFlags.PerRendererData ) ) == MaterialProperty.PropFlags.None )
			{
				if( properties[ i ].flags == MaterialProperty.PropFlags.NoScaleOffset )
					m_materialEditor.TexturePropertySingleLine( new GUIContent( properties[ i ].displayName ), properties[ i ] );
				else
					m_materialEditor.ShaderProperty( properties[ i ], properties[ i ].displayName );
			}
		}
	}
}
